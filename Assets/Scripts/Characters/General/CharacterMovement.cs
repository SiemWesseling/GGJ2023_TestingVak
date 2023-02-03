using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float acceleration, maxSpeed, descelleration;

    Rigidbody2D rb;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        DoMovement();
    }

    private void DoMovement()
    {
        if (direction == Vector2.zero)
        {
            ApplyDescelleration();
            return;
        }

        rb.AddForce(direction * acceleration);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void ApplyDescelleration()
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

