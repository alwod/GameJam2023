using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    public void DestroyDMGText()
    { 
        Destroy(gameObject.transform.parent.gameObject);
    }
}