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

    private State _state = State.Idle;

    public void IncreaseCherries()
    {
        cherries++;
        _text.text = cherries.ToString();
    }
    
    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Movement();
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
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, jumpForce);
            _state = State.Jumping;
        }


        AnimState();
        _animator.SetInteger(CurrentState, (int) _state);
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

    private enum State
    {
        Idle,
        Running,
        Jumping,
        Falling
    }
}