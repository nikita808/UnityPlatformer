using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundMask;
    private Collider2D _coll;
    private Rigidbody2D _rb;

    private bool facingLeft = true;

    private void Start()
    {
       _coll = GetComponent<Collider2D>();
       _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
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
                }
            }
            else
            {
                facingLeft = true;
               
            }
        }
    }

    
}
