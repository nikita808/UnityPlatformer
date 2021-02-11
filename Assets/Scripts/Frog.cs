#region

using UnityEngine;

#endregion

public class Frog : Enemy
{
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Falling = Animator.StringToHash("Falling");
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundMask;
    private Collider2D _coll;
    private Rigidbody2D _rb;

    private bool facingLeft = true;


    protected override void Start()
    {
        base.Start();
        _coll = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Anim.GetBool(Jumping))
        {
            if (_rb.velocity.y < .1f)
            {
                Anim.SetBool(Falling, true);
                Anim.SetBool(Jumping, false);
            }
        }


        if (_rb.IsTouchingLayers(groundMask) && Anim.GetBool(Falling))
        {
            Anim.SetBool(Falling, false);
        }
    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.localScale.x != 1)
            {
                transform.localScale = new Vector3(1, 1);
            }

            if (transform.position.x > leftCap)
            {
                // test if i am on the ground if so jump
                if (_coll.IsTouchingLayers(groundMask))
                {
                    // Jump
                    _rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    Anim.SetBool(Jumping, true);
                }
            }
            else
            {
                facingLeft = false;
            }

            // Test if we are beyond the left cap
        }
        else
        {
            if (transform.localScale.x != -1)
            {
                transform.localScale = new Vector3(-1, 1);
            }

            if (transform.position.x < rightCap)
            {
                // test if i am on the ground if so jump
                if (_coll.IsTouchingLayers(groundMask))
                {
                    // Jump
                    _rb.velocity = new Vector2(jumpLength, jumpHeight);
                    Anim.SetBool(Jumping, true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }
}