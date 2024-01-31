using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _explosionParticlePrefab;

    public int _health;
    private Material _matWhite;
    private Material _matDefault;
    SpriteRenderer _sr;
    public AudioSource _DeadSound;
    public float _time;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        _matDefault = _sr.material;
        _DeadSound = GetComponent<AudioSource>();
    }

    public void TakeDamage (int damage)
    {
        _sr.material = _matWhite;
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
        else
        {
            Invoke("ResetMaterial", 0.1f);
        }
    }
    void ResetMaterial()
    {
        _sr.material = _matDefault;
    }
    void Die()
    {
        _DeadSound.Play();
        Instantiate(_explosionParticlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, _time);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ReloadScene"))
        {
            Die();
        }
    }

}
