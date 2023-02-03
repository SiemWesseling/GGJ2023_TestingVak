using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootingCooldown;
    [SerializeField] private float bulletSpeed;
    private float timer = 0;
    private bool playerCanShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        timer = shootingCooldown;
    }

    private void FixedUpdate()
    {
        //Timer
        if (playerCanShoot == false)
        {
            timer += Time.deltaTime;
            if (timer >= shootingCooldown)
            {
                playerCanShoot = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Shooting
        if (Input.GetMouseButtonDown(0) && playerCanShoot)
        {
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (Vector2)((worldMousePos - transform.position));
            direction.Normalize();

            // Creates the bullet locally
            GameObject blt = Instantiate(bullet,transform.position + (Vector3)(direction * 0.5f),Quaternion.identity);

            // Adds velocity to the bullet
            blt.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            //Let the bullet face the right direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            blt.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            //Reset cooldown
            playerCanShoot = false;
            timer = 0;
        }
    }
}
