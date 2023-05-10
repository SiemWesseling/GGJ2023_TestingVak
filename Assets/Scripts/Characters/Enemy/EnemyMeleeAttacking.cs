using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class EnemyMeleeAttacking : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float attackCoolDown;

    private float timer = 0;
    private bool enemyCanAttack = true;

    private void Start()
    {
        TestingConnect.AnalitycsInitializedSucces += (object sender, EventArgs e) =>
        {
            Debug.Log("Analytics Connected");
            MeleeAttackTestData();
        };
    }
    private void MeleeAttackTestData()
    {
        AnalyticsService.Instance.CustomData("GotHitFromVirus", new Dictionary<string, object> { { "TotalHitsFromVirus", 0 } });

        if(TestingConnect.IsInitialized)
        {
            
        }
    }
    private void FixedUpdate()
    {
        //Timer
        if (enemyCanAttack == false)
        {
            timer += Time.deltaTime;
            if (timer >= attackCoolDown)
            {
                enemyCanAttack = true;
                timer = 0;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemyCanAttack)
        {
            if(collision.gameObject.tag == "Player")
            {
                MeleeHitPlayer(collision.gameObject.GetComponent<HealthManager>());
                enemyCanAttack = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (enemyCanAttack)
        {
            if(collision.gameObject.tag == "Player")
            {
                MeleeHitPlayer(collision.gameObject.GetComponent<HealthManager>());
                enemyCanAttack = false;
            }
        }
    }


    void MeleeHitPlayer(HealthManager health)
    {
        health.onLostHealth.Invoke(damage);
    }
}
