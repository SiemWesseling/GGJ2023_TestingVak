using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerInput : MonoBehaviour
{
    CharacterMovement movement;

    [Header("Keys")]
    [SerializeField] KeyCode upKey, rightKey, downKey, leftKey;

    void Start()
    {
        movement = GetComponent<CharacterMovement>();
    }

    private void FixedUpdate()
    {
        movement.SetDirection(GetDirection());
    }

    Vector2 GetDirection()
    {
        Vector2 toReturn = new Vector2();
        if (Input.GetKey(upKey))
        {
            toReturn += Vector2.up;
        }
        if (Input.GetKey(downKey))
        {
            toReturn += Vector2.down;
        }
        if (Input.GetKey(rightKey))
        {
            toReturn += Vector2.right;
        }
        if (Input.GetKey(leftKey))
        {
            toReturn += Vector2.left;
        }
        return toReturn;
    }
}
