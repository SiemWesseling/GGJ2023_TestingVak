using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected float acceleration, maxSpeed, descelleration;
    
    [SerializeField] protected Animator animator;

    protected Rigidbody2D rb;

    protected Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            animator = GameObject.FindWithTag("PlayerSprite").GetComponent<Animator>();
        }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        DoMovement();
    }

    protected virtual void DoMovement()
    {
        if (direction == Vector2.zero)
        {
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }

            ApplyDescelleration();
            return;
        }
        
        if (animator != null)
        {
            animator.SetBool("isMoving", true);
        }

        rb.AddForce(direction * acceleration);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    protected virtual void ApplyDescelleration()
    {
        if (rb.velocity.magnitude != 0f) {
            rb.velocity -= rb.velocity.normalized *  0.01f * descelleration;
        }
        if (rb.velocity.magnitude < maxSpeed / 10f)
        {
            rb.velocity = Vector2.zero;
        }
    }
}

