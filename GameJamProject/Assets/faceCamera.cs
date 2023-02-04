using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
