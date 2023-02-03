using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTree : MonoBehaviour
{
    
    private PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindObjectOfType<PlayerController> ();
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 lookAt = Player.transform.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    }
}
