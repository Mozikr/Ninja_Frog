using UnityEngine;
using UnityEngine.SceneManagement;

public class GetHit : MonoBehaviour
{
    public int health = 20;
    [SerializeField] private GameObject _explosionParticlePrefab;
    [SerializeField] private GameObject _diedexposion;
    public int _indexToReload;
    public float _timetoDead;
    public float _SawTimeToDead;
    private HearManager _hearts;

    private void Start()
    {
        _hearts = FindObjectOfType<HearManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            _hearts.MinusHearts(1);
            health --;
        }
        else if (health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            Destroy(gameObject, _timetoDead);
            SceneManager.LoadScene(_indexToReload);
        }
        

        if (other.gameObject.CompareTag("ReloadScene"))
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            Instantiate(_explosionParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, _timetoDead);
        }
        if(other.gameObject.CompareTag("Saw"))
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            Instantiate(_explosionParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, _SawTimeToDead);
        }
    }
}
   
