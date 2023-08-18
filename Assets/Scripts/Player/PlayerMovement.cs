using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    
    private Rigidbody2D _rigidBody;

    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 movementDirection;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(moveX, moveY).normalized;

        if (movementDirection.x != 0)
            lastHorizontalVector = movementDirection.x;

        if (movementDirection.y != 0)
            lastVerticalVector = movementDirection.y;
    }

    private void Move()
    {
        _rigidBody.velocity = new Vector2(movementDirection.x * movementSpeed, movementDirection.y * movementSpeed);
    }
}