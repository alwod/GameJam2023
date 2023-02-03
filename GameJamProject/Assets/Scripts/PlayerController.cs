using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Align to player position
        transform.position = player.transform.position;
        
        // UP
        if (Input.GetKey(KeyCode.W))
        {
            
        }

        // Down
        if (Input.GetKey(KeyCode.S))
        {
            
        }
        
        // Left
        if (Input.GetKey(KeyCode.A))
        {
            
        }
        
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            
        }
    }
}
