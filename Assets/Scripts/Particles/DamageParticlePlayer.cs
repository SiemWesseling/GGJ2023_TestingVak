using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageParticlePlayer : MonoBehaviour
{
    HealthManager health;
    ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponentInParent<HealthManager>();
        particles = GetComponent<ParticleSystem>();
        health.onLostHealth.AddListener(PlayParticles);
    }

    void PlayParticles(float x)
    {
        particles.Play();
    }
}
