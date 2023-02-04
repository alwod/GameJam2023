using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerPosition;
    public float cameraHeight = 25;
    public Quaternion cameraAngle = Quaternion.Euler(65, 0, 0);
    public float zOffset = 10;

    void Update()
    {
        // Make camera look down ad an angle, not straight down.
        transform.rotation = cameraAngle;
        // Make camera follow the player
        transform.position = playerPosition.transform.position + new Vector3(0, cameraHeight, -zOffset);
    }
}
