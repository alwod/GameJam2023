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

    public GameObject audioManager;


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
            transform.position = new Vector3(-2, 1, 0) + transform.parent.position;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.position = new Vector3(2, 1, 0) + transform.parent.position;
        }

        ParticleSystem.Particle[] partSystem = new ParticleSystem.Particle[ps.particleCount]; // Get array of particles within the particle system.
        int numParticlesAlive = ps.GetParticles(partSystem);  // Get number of currently active particles.
        
        ParticleSystem.Particle[] validParticles = GetInRangeParticles(partSystem);

        ps.SetParticles(validParticles, validParticles.Length); // Set particle system to use particles in arrays, aka omit particles that were outside the range.s

        isFiring = Input.GetKey(KeyCode.Space); // If using primary fire key, set firing to true, otherwise false. 

        // Shoot flamethrower if firing.
        if (isFiring)
        {
            audioManager.GetComponent<AudioManager>().Play("Flamethrower");
            ps.Play();
            fireTime += Time.deltaTime;
        }
        else
        {
            ps.Stop();
            fireTime = 0f;
            // Stop flamethrower sound effect
            audioManager.GetComponent<AudioManager>().Stop("Flamethrower");
        }
    }

    /// <summary>
    /// For each active particle in the flamethrower, check if it's exceeded the flamethrower's max range. Exclude out of range particles.
    /// </summary>
    /// <param name="partSystem">Particle system that controls the flamethrower particles.</param>
    /// <returns>Array of particles within the range threshold.</returns>
    private ParticleSystem.Particle[] GetInRangeParticles(ParticleSystem.Particle[] partSystem)
    {
        List<ParticleSystem.Particle> validParticles = new List<ParticleSystem.Particle>();
        
        // For each particle in the array...
        for (int i = 0; i < partSystem.Length; i++)
        {
            // If the particle is within range, add it to the array.
            if (Vector3.Distance(emitterPos, partSystem[i].position) < flameMaxRange)
            {
                validParticles.Add(partSystem[i]);
            }
        }
        return validParticles.ToArray();
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
