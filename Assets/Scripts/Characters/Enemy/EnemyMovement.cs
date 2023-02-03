using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class EnemyMovement : MonoBehaviour
{
    CharacterMovement movement;
    
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<CharacterMovement>();
    }

    // FixedUpdate is called 60 times per second
    void FixedUpdate()
    {
        movement.SetDirection(FollowPlayer());
    }

    Vector2 FollowPlayer()
    {
        //todo: dont push player forward on contact
        Vector2 directionToPlayer = new Vector2();
        directionToPlayer = (this.transform.position - GameManager.Instance.player.transform.position).normalized;
        return -directionToPlayer;
    }
}
