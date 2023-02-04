using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class EnemyMovement : MonoBehaviour
{
    CharacterMovement movement;
    public bool canMove = true;
    
    [SerializeField] private Rigidbody2D rb;
    
    void Start()
    {
        movement = GetComponent<CharacterMovement>();
        
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            movement.SetDirection(FollowPlayer());
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    Vector2 FollowPlayer()
    {
        //todo: dont push player forward on contact
        Vector2 directionToPlayer = new Vector2();
        directionToPlayer = (this.transform.position - GameManager.Instance.player.transform.position).normalized;
        return -directionToPlayer;
    }
}
