using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public event Action<bool> onPlayerIdleChange;
    public event Action<bool> onPlayerWalkChange;

    private bool isWalking = false;
    private bool isIdle = true;

    [Header("References")]
    public PlayerData playerData;

    public SelectWeapon[] selectWeapon;

    public ConveyorBelt conveyorBelt;

    public PlayerFlip playerFlip;

    private void Start()
    {
        playerData.ResetPlayerStacks();
        playerData.rigidBody = GetComponent<Rigidbody>();
        playerData.playerCollider = GetComponent<BoxCollider>();
        playerData.playerDashMaterial = GetComponent<Renderer>().material;
        playerData.dashColor = playerData.playerDashMaterial.color;
        playerData.activeMoveSpeed = playerData.speed;
    }

    private void FixedUpdate()
    {
        MoveLogic();
        Move();
    }

    private void MoveLogic()
    {
        RestartIdleAnimation();

        if (playerData.movementDirection.x != 0)
        {
            isIdle = false;
            onPlayerIdleChange?.Invoke(isIdle);

            playerFlip.FlipPlayerX();

            isWalking = true;
            onPlayerWalkChange?.Invoke(isWalking);
        }
        if (playerData.movementDirection.y != 0)
        {
            isIdle = false;
            onPlayerIdleChange?.Invoke(isIdle);

            playerFlip.FlipPlayerY();

            isWalking = true;
            onPlayerWalkChange?.Invoke(isWalking);
        }
    }

    public void Movement(InputValue value) 
    {
        if (playerData._isDead == false) 
        {
            var movementInput = value.Get<Vector2>();

            playerData.movementDirection = new Vector2(movementInput.x, movementInput.y).normalized;
        }

        else 
        {
            var movementInput = value.Get<Vector2>();

            playerData.movementDirection = new Vector2(0, 0).normalized;
        }
    }

    private void Move()
    {
        if (conveyorBelt.isOnConveyorBelt)
        {
            playerData.rigidBody.velocity = new Vector2((playerData.movementDirection.x * playerData.activeMoveSpeed) + (conveyorBelt.conveyorBeltSpeed * Time.deltaTime), playerData.movementDirection.y * playerData.activeMoveSpeed);
        }
        else
        {
            playerData.rigidBody.velocity = new Vector2(playerData.movementDirection.x * playerData.activeMoveSpeed, playerData.movementDirection.y * playerData.activeMoveSpeed);
        }
    }

    private void RestartIdleAnimation()
    {
        isWalking = false;
        isIdle = true;
        onPlayerIdleChange?.Invoke(isIdle);
        onPlayerWalkChange?.Invoke(isWalking);
    }
}