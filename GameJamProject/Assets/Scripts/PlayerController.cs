using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    private Rigidbody _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        // _rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var movement = Vector3.zero;

        var speed = movementSpeed * Time.deltaTime;
        // UP
        if (Input.GetKey(KeyCode.W))
        {
            movement.z += movementSpeed;
        }
        
        // Down
        if (Input.GetKey(KeyCode.S))
        {
            movement.z += -movementSpeed;
        }
        
        // Left
        if (Input.GetKey(KeyCode.A))
        {
            movement.x += -movementSpeed;
            _spriteRenderer.flipX = true;
        }
        
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += movementSpeed;
            _spriteRenderer.flipX = false;
        }

        if (movement == Vector3.zero) _animator.SetBool("isMoving",false);
        else _animator.SetBool("isMoving",true);

        _rigidbody.MovePosition(transform.position + movement * speed);
    }

    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
