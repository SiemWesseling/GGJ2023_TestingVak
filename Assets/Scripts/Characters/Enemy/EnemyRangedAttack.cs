using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyRangedAttack : MonoBehaviour
{

    public UnityEvent onPlayerSpotted = new UnityEvent();
    public UnityEvent onPlayerLost = new UnityEvent();
    
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private EnemyMovement enemyMovement;

    private GameObject player;

    private bool canShoot = false;

    [SerializeField] private float bulletCooldown = 1f;
    private float bulletTimer = 0f;
    [SerializeField] private float bulletSpeed = 10f;
    
    [SerializeField] private GameObject enemyBulletPrefab;


    private int rangeAttacks = 0;
    private void Start()
    {
        onPlayerSpotted.AddListener(PlayerSpotted);
        onPlayerLost.AddListener(PlayerLost);
        
        if(characterMovement == null)
        {
            characterMovement = GetComponent<CharacterMovement>();
        }
        if(enemyMovement == null)
        {
            enemyMovement = GetComponent<EnemyMovement>();
        }
        
        player = GameObject.FindWithTag("Player");
    }
    public void OnAnalyticsInitializedSucces()
    {
        //// Unsubscribe from the event
        //TestingConnect.AnalitycsInitializedSucces -= OnAnalyticsInitializedSucces;
        Debug.Log("Range Attack Test");

        // Now you can log events to the Analytics service
        AnalyticsService.Instance.CustomData("GotHitFromVirus", new Dictionary<string, object> {
            { "TotalHitsFromVirus",  rangeAttacks}
        });
    }
    private void FixedUpdate()
    {
        if (canShoot)
        {
            ShootMode();
        }
        else
        {
            canShoot = false;
        }
    }
    
    private void PlayerSpotted()
    {
        enemyMovement.canMove = false;
        characterMovement.SetDirection(Vector2.zero);
        canShoot = true;
    }

    private void PlayerLost()
    {
        canShoot = false;
        enemyMovement.canMove = true;
    }

    private void ShootMode()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > bulletCooldown)
        {
            Shoot();
            bulletTimer = 0f;
        }
    }

    private void Shoot()
    {
        if (!GameManager.Instance.paused)
        {
            rangeAttacks++;
            Vector2 direction = (Vector2)(player.transform.position - transform.position).normalized;
            GameObject eBullet = Instantiate(enemyBulletPrefab, transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);
            
            eBullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            eBullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            OnAnalyticsInitializedSucces();
        }
    }
}
