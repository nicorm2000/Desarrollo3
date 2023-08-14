using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    
    private Rigidbody2D _rigidBody;
    private Vector2 _movementDirection;

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

        _movementDirection = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        _rigidBody.velocity = new Vector2(_movementDirection.x * movementSpeed, _movementDirection.y * movementSpeed);
    }
}