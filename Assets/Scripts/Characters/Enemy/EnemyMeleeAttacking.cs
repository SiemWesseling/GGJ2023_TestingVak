using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttacking : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float attackCoolDown;

    private float timer = 0;
    private bool enemyCanAttack = true;

    private void FixedUpdate()
    {
        //Timer
        if (enemyCanAttack == false)
        {
            timer += Time.deltaTime;
            if (timer >= attackCoolDown)
            {
                enemyCanAttack = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            MeleeHitPlayer(collision.gameObject.GetComponent<HealthManager>());
        }
    }

    void MeleeHitPlayer(HealthManager health)
    {
        health.onLostHealth.Invoke(damage);
    }
}
