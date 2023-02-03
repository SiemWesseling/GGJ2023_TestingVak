using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private float shootingCooldown = 1f;
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
            blt.GetComponent<Rigidbody2D>().velocity = direction * 2;

            //Reset cooldown
            playerCanShoot = false;
            timer = 0;
        }
    }
}
