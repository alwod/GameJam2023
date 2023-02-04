using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerPosition;
    public float cameraHeight;

    private void Start()
    {
        // Make camera face down
        transform.forward = Vector3.down;
    }
    
    void Update()
    {
        // Make camera follow the player
        transform.position = playerPosition.transform.position + new Vector3(0, cameraHeight, 0);
    }
}
