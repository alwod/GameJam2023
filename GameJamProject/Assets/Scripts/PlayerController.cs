using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var speed = movementSpeed * Time.deltaTime;
        // UP
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + new Vector3(0, 0, speed);
        }

        // Down
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + new Vector3(0, 0, -speed);
        }
        
        // Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + new Vector3(-speed, 0, 0);
        }
        
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + new Vector3(speed, 0, 0);
        }
    }
}
