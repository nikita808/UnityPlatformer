#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class PlayerController : MonoBehaviour
{
    private static readonly int CurrentState = Animator.StringToHash("State");
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    private Animator _animator;
    private Collider2D _coll;
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private int cherries = 0;
    [SerializeField] private Text _text;
    [SerializeField] private float hurtForce = 10f;

    private State _state = State.Idle;

    public void IncreaseCherries()
    {
        cherries++;
        _text.text = cherries.ToString();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") )
        {
            if (_state == State.Falling)
            {
                Destroy(collision.gameObject);
                Jump();
            }
            else
            {
                _state = State.Hurt;
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    _rigidBody2D.velocity = new Vector2(-hurtForce, _rigidBody2D.velocity.y);
                }
                else
                {
                    _rigidBody2D.velocity = new Vector2(hurtForce, _rigidBody2D.velocity.y);
                }
            }
            
        }
    }
    
    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_state != State.Hurt)
        {
            Movement();
        }
        
        AnimState();
        _animator.SetInteger(CurrentState, (int) _state);
    }
    

    private void Movement()
    {
        var hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0)
        {
            _rigidBody2D.velocity = new Vector2(-speed, _rigidBody2D.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        else if (hDirection > 0)
        {
            _rigidBody2D.velocity = new Vector2(speed, _rigidBody2D.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }


        if (Input.GetButtonDown("Jump") && _coll.IsTouchingLayers(groundMask))
        {
            Jump();
        }
        
    }

    private void AnimState()
    {
        if (_state == State.Jumping)
        {
            if (_rigidBody2D.velocity.y < .1f)
            {
                _state = State.Falling;
            }
        }

        else if (_state == State.Falling)
        {
            if (_coll.IsTouchingLayers(groundMask))
            {
                _state = State.Idle;
            }
        }
        else if (_state == State.Hurt)
        {
            if (Mathf.Abs(_rigidBody2D.velocity.x) < .1f)
            {
                _state = State.Idle;
            }
        }

        else if (Mathf.Abs(_rigidBody2D.velocity.x) > 2f)
        {
            // Moving
            _state = State.Running;
        }
        else
        {
            _state = State.Idle;
        }
    }

    private void Jump()
    {
        _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, jumpForce);
        _state = State.Jumping;
    }

    private enum State
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Hurt
    }
}