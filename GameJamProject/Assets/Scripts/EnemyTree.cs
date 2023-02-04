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
    void FixedUpdate ()
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        Rigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }
}
