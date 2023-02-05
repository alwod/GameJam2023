using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{

    private ParticleSystem ps;
    [SerializeField] private float flameMaxRange; // Particles are destroyed beyond this range.

    private Vector3 emitterPos; // Position of the flamethrower base, used to range check vs particle positions.
    private bool isFiring;
    private float fireTime; // How long the flamethrower has been firing for.

    private SpriteRenderer _spriteRenderer; // Reference to player sprite to rotate flames when player rotates.


    private void Start()
    {
        emitterPos = transform.position;
        ps = GetComponent<ParticleSystem>();
        flameMaxRange = 15f;

        _spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();

        fireTime = 0f;

        GameObject[] enemies = GameObject.Find("_GAME MANAGER").GetComponent<GameManager>().GetEnemies();

        foreach (GameObject enemy in enemies)
        {
            ps.trigger.AddCollider(enemy.GetComponent<BoxCollider>());
        }
    }

    private void Update()
    {
        if (_spriteRenderer.flipX)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.position = new Vector3(-2f, 1.5f, 0) + transform.parent.position;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.position = new Vector3(2f, 1.5f, 0) + transform.parent.position;
        }

        ParticleSystem.Particle[] partSystem = new ParticleSystem.Particle[ps.particleCount]; // Get array of particles within the particle system.
        int numParticlesAlive = ps.GetParticles(partSystem);  // Get number of currently active particles.

        // Set particle system start speed to be between 8 and 12.
        

        isFiring = Input.GetKey(KeyCode.Space); // If using primary fire key, set firing to true, otherwise false. 

        // Shoot flamethrower if firing.
        if (isFiring) 
        { 
            ps.Play();
            fireTime += Time.deltaTime;
        }
        else
        {
            ps.Stop();
            fireTime = 0f;
        }

        if (fireTime > 0)
        {
            var velocity = ps.velocityOverLifetime;
            // get parent's transform direction this frame.
            var direction = transform.parent.transform.forward;
            // scale flamethrower with direction
            velocity.xMultiplier = direction.x * 10;
            velocity.yMultiplier = direction.y * 10;
            velocity.zMultiplier = direction.z * 10;


        }
    }


    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enteredParts = new List<ParticleSystem.Particle>();

        int enterNo = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enteredParts);

        GameObject[] enemies = GameObject.Find("_GAME MANAGER").GetComponent<GameManager>().GetActiveEnemies();

        foreach (ParticleSystem.Particle part in enteredParts)
        {
            foreach (GameObject enemy in enemies)
            {
                Collider col = enemy.GetComponent<Collider>();
                if (col.bounds.Contains(part.position))
                {
                    enemy.GetComponent<EnemyTree>().TakeDamage(1, "Flame");
                    return;
                }
            }
        }
    }

    /// <summary>
    /// After a delay, disable all the box colliders. Used to disable collider after lingering flames have dissipated.
    /// </summary>
    /// <param name="delay">Time in seconds to delay the disable.</param>
    IEnumerator bcDisableDelayed(float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        foreach (BoxCollider bc in GetComponents<BoxCollider>())
        {
            bc.enabled = false;
        }
        
        yield return null;
    }
}
