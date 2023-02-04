using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
   float modifiedMaxSpeed { get { return maxSpeed * PlayerStats.Instance.speedMult; } }

    protected override void DoMovement()
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

        if (rb.velocity.magnitude > modifiedMaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * modifiedMaxSpeed;
        }
    }

    protected override void ApplyDescelleration()
    {
        if (rb.velocity.magnitude != 0f)
        {
            rb.velocity -= rb.velocity.normalized * 0.01f * descelleration;
        }
        if (rb.velocity.magnitude < modifiedMaxSpeed / 10f)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
