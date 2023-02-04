using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int maximumHealth = 100;
    public GameObject currentHealthObject;

    [SerializeField] private int Health;
    [SerializeField]
    private float movementSpeed;

    private Rigidbody _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool damageCooldown;
    private TextMeshProUGUI currentHealthUI;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        Health = maximumHealth;
        damageCooldown = false;
        currentHealthUI = currentHealthObject.GetComponent<TextMeshProUGUI>();
        setHealth();
        // _rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void FixedUpdate()
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
            _spriteRenderer.flipX = true;
        }
        
        // Right
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += movementSpeed;
            _spriteRenderer.flipX = false;
        }

        if (movement == Vector3.zero) _animator.SetBool("isMoving",false);
        else _animator.SetBool("isMoving",true);

        _rigidbody.MovePosition(transform.position + movement * speed);
    }

    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
    
    IEnumerator DisableDamage()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(1);
        damageCooldown = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !damageCooldown)
        {
            Debug.Log("Ouch, I'm being hurt.");
            Health -= 10;
            damageCooldown = true;
            StartCoroutine(DisableDamage());
            setHealth();
        }

        if (Health <= 0)
        {
            OnDefeat();
        }
    }

    void OnDefeat()
    {
        Debug.Log("Game over mate, you lost");
        gameObject.SetActive(false);
    }

    void setHealth()
    {
        currentHealthUI.SetText("Health: " + Health + "/" + maximumHealth);
    }
}
