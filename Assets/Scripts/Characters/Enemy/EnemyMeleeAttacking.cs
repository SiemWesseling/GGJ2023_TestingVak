using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine.SceneManagement;

public class EnemyMeleeAttacking : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float attackCoolDown;

    private float timer = 0;
    private bool enemyCanAttack = true;
    private int meleeAttacks;

    private void Start()
    {
        UnityServices.InitializeAsync();
    }

    private void OnAnalyticsInitializedSucces()
    {
        //// Unsubscribe from the event
        //TestingConnect.AnalitycsInitializedSucces -= OnAnalyticsInitializedSucces;
        Debug.Log("Melee Attack Test");

        // Now you can log events to the Analytics service
        AnalyticsService.Instance.CustomData("GotHitFromBacteria", new Dictionary<string, object> {
            { "SceneName", SceneManager.GetActiveScene().name },
            { "TotalHitsFromBacteria",  meleeAttacks}
        });
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
                meleeAttacks++;
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
