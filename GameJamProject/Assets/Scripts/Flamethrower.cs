using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{

    private ParticleSystem ps;
    [SerializeField] private float flameMaxRange;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        ps = GetComponent<ParticleSystem>();
        flameMaxRange = 40f;
    }

    private void Update()
    {
        ParticleSystem.Particle[] partSystem = new ParticleSystem.Particle[ps.particleCount];
        int numParticlesAlive = ps.GetParticles(partSystem);

        ParticleSystem.Particle[] distanceParticles = partSystem.Where(p => Vector3.Distance(startPos, p.position) < flameMaxRange).ToArray();
        
        ps.SetParticles(distanceParticles, distanceParticles.Length);
    }

}
