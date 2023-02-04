using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTree : MonoBehaviour
{
    public int speed = 5;
    private PlayerController Player;
    private Rigidbody Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindObjectOfType<PlayerController> ();
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 lookAt = Player.transform.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
        Rigidbody.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
    }
}
