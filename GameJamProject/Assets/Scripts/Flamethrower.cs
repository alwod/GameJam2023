using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        flameMaxRange = 40f;

        _spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();

        fireTime = 0f;
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
            ps.Play();
            fireTime += Time.deltaTime;
        }
        else
        {
            ps.Stop();
            fireTime = 0f;
        }

        if (fireTime > 0.5f)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
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

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Debug.Log("BURN");
                break;
        }
    }
}
