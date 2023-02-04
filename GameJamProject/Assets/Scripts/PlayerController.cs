using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
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
        }
        
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += movementSpeed;
        }

        transform.position += movement * speed;
    }
}
