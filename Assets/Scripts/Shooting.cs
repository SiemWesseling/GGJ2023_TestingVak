using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public ShootEvent onShoot = new ShootEvent();

    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootingCooldown;
    //For upgrades
    float upgradedShootingCooldown { get { return shootingCooldown / PlayerStats.Instance.fireRateMult; } }
    

    [SerializeField] private float bulletSpeed;
    //For upgrades
    float upgradedBulletSpeed { get { return bulletSpeed * PlayerStats.Instance.ProjectileSpeedMult; } }

    private float timer = 0;
    private bool playerCanShoot = true;

    void Start()
    {
        timer = upgradedShootingCooldown;
    }

    private void FixedUpdate()
    {
        //Timer
        if (playerCanShoot == false)
        {
            timer += Time.deltaTime;
            if (timer >= upgradedShootingCooldown)
            {
                playerCanShoot = true;
            }
        }
    }

    void Update()
    {
        //Shooting
        if (Input.GetMouseButton(0) && playerCanShoot && GameManager.Instance.paused == false)
        {
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (Vector2)((worldMousePos - transform.position));
            direction.Normalize();

            // Creates the bullet locally
            GameObject blt = Instantiate(bullet, transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);

            // Adds velocity to the bullet
            blt.GetComponent<Rigidbody2D>().velocity = direction * upgradedBulletSpeed;

            onShoot.Invoke(bullet, transform.position, direction * upgradedBulletSpeed);

            //Let the bullet face the right direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            blt.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            Bullet bulletScript = blt.GetComponent<Bullet>();
            bulletScript.transform.localScale *= PlayerStats.Instance.ProjectileSizeMult;
            bulletScript.damage += 10f * PlayerStats.Instance.bulletDamage;
            
            //Reset cooldown
            playerCanShoot = false;
            timer = 0;
        }
    }
}
