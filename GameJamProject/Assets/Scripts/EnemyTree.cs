using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemyTree : MonoBehaviour
{
    public int speed = 5;
    public int maxHealth = 5000;
    private PlayerController Player;
    private Rigidbody Rigidbody;
    private GameManager GameManager;
    private NavMeshAgent meshAgent;
    private SpriteRenderer SpriteRenderer;

    public int Health // Enemy's current health.
    {
        get;
        private set;
    }

    public int FlameTickCount // Number of stored flame ticks that is yet to be applied to the enemy. Damage over time.
    {
        get;
        private set;
    }

    private float flameTickDamageDelay; // Enforces a 1 second delay per instance of damage from a flame tick.
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindObjectOfType<PlayerController> ();
        Rigidbody = GetComponent<Rigidbody>();
        GameManager = GameObject.FindObjectOfType<GameManager>();
        meshAgent = GetComponent<NavMeshAgent>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        Health = maxHealth;
        FlameTickCount = 0;
        flameTickDamageDelay = 0;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        /*
        Vector3 direction = (Player.transform.position - transform.position).normalized;

        // lock rotation on x axis to 45 degree angle multipliers only
        transform.rotation = Quaternion.Euler(45 * Mathf.Round(transform.rotation.eulerAngles.x / 45), transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        Rigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
        */

        Vector3 playerPosition = Player.transform.position;
        meshAgent.SetDestination(playerPosition);
        Vector3 playerDirection = (playerPosition - transform.position).normalized;
        if (playerDirection.x < 0) SpriteRenderer.flipX = true;
        else SpriteRenderer.flipX = false;
    }

    void Update()
    {
        // If enemy is still on fire and the damage delay has ended...
        if (FlameTickCount > 0 && flameTickDamageDelay < 0f)
        {
            var damageUI = GameObject.Instantiate(GameManager.GetDMGSprite(), gameObject.transform);
            damageUI.transform.Translate(new Vector3(Random.Range(-3f, 3f), Random.Range(0, 3f), Random.Range(-3f, 3f)));
            // Reduce health and stored ticks by 1/4 stored ticks (or 1 when at <5 ticks), reset the delay timer.
            if (FlameTickCount <= 5)
            {
                Health--;
                FlameTickCount--;
                damageUI.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("1!");
                damageUI.GetComponent<Rigidbody>().AddForce(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f), ForceMode.Impulse);
            }
            else
            {
                damageUI.transform.GetChild(0).GetComponent<TextMeshPro>().SetText((FlameTickCount / 4).ToString() + "!");
                damageUI.GetComponent<Rigidbody>().AddForce(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f), ForceMode.Impulse);
                Health -= FlameTickCount / 4;
                FlameTickCount -= FlameTickCount / 4;

            }

            if (Health <= 0) { gameObject.SetActive(false); }
            flameTickDamageDelay = 1f;
            Debug.Log("ON FIRE: " + Health + "\nTICKS REMAINING:" + FlameTickCount);
        }

        flameTickDamageDelay -= Time.deltaTime;
    }

    /// <summary>
    /// Enemy handles incoming damage. Also handles receiving debuffs from attacks.
    /// </summary>
    /// <param name="damage">Incoming damage value.</param>
    /// <param name="debuff">(Optional) additional harmful effect brought onto the enemy. Defaults to no debuff if not specified.</param>
    public void TakeDamage(int damage, string debuff = "")
    {
        Health -= damage;
        
        if (Health <= 0 && gameObject.activeSelf)
        {
            OnDeath();
        }

        switch (debuff)
        {
            // Incoming fire attacks increase the tick count, making the enemy take damage over time.
            case "Flame":
                FlameTickCount+= 2;
                break;
        }

        var damageUI = GameObject.Instantiate(GameManager.GetDMGSprite(), gameObject.transform);
        damageUI.transform.Translate(new Vector3(Random.Range(-3f, 3f), Random.Range(0, 3f), Random.Range(-3f, 3f)));
        damageUI.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(damage.ToString());

        Debug.Log(Health);

    }

    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    void OnDeath()
    {
        Debug.Log("Oh no. I am dying. Help.");
        gameObject.SetActive(false);
        GameManager.defeatedEnemies++;
    }
}
