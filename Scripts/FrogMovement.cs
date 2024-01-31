using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    [SerializeField] int _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] private GameObject _collectedObjectApple;
    [SerializeField] private GameObject _collectedObjectKiwi;
    [SerializeField] private GameObject _collectedObjectMelon;
    [SerializeField] private GameObject _collectedObjectAnanas;
    Rigidbody2D _rb2d;
    public Transform _groundCheckPoint;
    public float _groundCheckRadius;
    public LayerMask _groundLayer;
    private bool _isTouchingGround;
    private float _horizontalMove;
    public Animator _animator;
    private bool _isFacingRight = true;
    public int health = 10;
    private PointManager _pointsManager;
    public ParticleSystem _dust;


    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _pointsManager = FindObjectOfType<PointManager>();
    }
    private void Update()
    {

        _horizontalMove = Input.GetAxisRaw("Horizontal");

        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        if (!_isTouchingGround)
        {
            _animator.SetBool("IsJumping", true);
        }
        else
        {
            _animator.SetBool("IsJumping", false);
        }
     
       if(_isFacingRight && _horizontalMove < 0)
        {
            Flip();
        }else if(!_isFacingRight && _horizontalMove > 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.W) && _isTouchingGround)
        {
            CreateDust();
            FindObjectOfType<AudioManager>().Play("Jump");
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);
        }

        _isTouchingGround = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);

    }
    private void FixedUpdate()
    {

        if (Input.GetKey("d"))
        {
            _rb2d.velocity = new Vector2(_speed, _rb2d.velocity.y);
        }
        else if (Input.GetKey("a"))
        {
            _rb2d.velocity = new Vector2(-_speed, _rb2d.velocity.y);
        }
        else
        {
            _rb2d.velocity = new Vector2(0, _rb2d.velocity.y);
        }
    }
    void Flip()
    {
        CreateDust();
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0.0f, -180f, 0.0f);
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            FindObjectOfType<AudioManager>().Play("PickingUpFruit");
            Instantiate(_collectedObjectApple, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            _pointsManager.AddPoints(1);

        }
        else if (collision.gameObject.CompareTag("Pineapple"))
        {
            FindObjectOfType<AudioManager>().Play("PickingUpFruit");
            Instantiate(_collectedObjectAnanas, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            _pointsManager.AddPoints(2);
        }
        else if (collision.gameObject.CompareTag("Melon"))
        {
            FindObjectOfType<AudioManager>().Play("PickingUpFruit");
            Instantiate(_collectedObjectMelon, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            _pointsManager.AddPoints(5);
        }
        else if (collision.gameObject.CompareTag("Kiwi"))
        {
            FindObjectOfType<AudioManager>().Play("PickingUpFruit");
            Instantiate(_collectedObjectKiwi, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            _pointsManager.AddPoints(3);
        }

    }
    void CreateDust()
    {
        _dust.Play();
    }


}
