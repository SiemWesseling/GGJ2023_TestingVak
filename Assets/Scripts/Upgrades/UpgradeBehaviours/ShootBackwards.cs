using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upgradebehaviours
{
    [RequireComponent(typeof(Shooting))]
    public class ShootBackwards : UpgradeBehaviour
    {
        Shooting shooting;
        private void Start()
        {
            shooting = GetComponent<Shooting>();
            shooting.onShoot.AddListener(ShootAsWell);
        }

        private void ShootAsWell(GameObject bullet, Vector2 startPos, Vector2 velocity)
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = startPos;
            newBullet.GetComponent<Rigidbody2D>().velocity = -velocity;

            //Let the bullet face the right direction
            float angle = Mathf.Atan2(-velocity.y, -velocity.x) * Mathf.Rad2Deg;
            newBullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}