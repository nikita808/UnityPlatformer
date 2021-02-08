using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody2D;
    [SerializeField] private Animator _animator;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    private void Start()
    {
        
    }

    private void Update()
    {
        var hDirection = Input.GetAxis("Horizontal");
        
        if (hDirection < 0)
        {
            _rigidBody2D.velocity = new Vector2(-5, _rigidBody2D.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            _animator.SetBool(IsRunning, true);
            
        }
        
        else if (hDirection > 0)
        {
            _rigidBody2D.velocity = new Vector2(5, _rigidBody2D.velocity.y);
            transform.localScale = new Vector2(1, 1);
            _animator.SetBool(IsRunning, true);
        }

        else
        {
            _animator.SetBool(IsRunning, false);
        }
        
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.Space))
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, 10f);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, -10f);
        }
    }
}
