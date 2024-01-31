using UnityEngine;

public class EnemySideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool _movingLeft;
    private float _leftEdge;
    private float _rightEdge;

    private void Awake()
    {
        _leftEdge = transform.position.x - movementDistance;
        _rightEdge = transform.position.x + movementDistance;
    }

    void Update()
    {
        if (_movingLeft)
        {
            if(transform.position.x > _leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                _movingLeft = false;
                transform.Rotate(0.0f, 180f, 0.0f);
            }
        }
        else
        {
            if (transform.position.x < _rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                _movingLeft = true;
                transform.Rotate(0.0f, -180f, 0.0f);
            }
        }
    }
    }
