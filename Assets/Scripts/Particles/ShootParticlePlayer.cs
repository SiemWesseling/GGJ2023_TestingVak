using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootParticlePlayer : MonoBehaviour
{
    ParticleSystem particles;
    Shooting shooting;
    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        shooting = GetComponentInParent<Shooting>();
        shooting.onShoot.AddListener(PlayParticles);
    }

    void PlayParticles(GameObject o, Vector2 p, Vector2 d)
    {
        particles.Play();
    }
}
