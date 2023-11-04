using System;
using UnityEngine;

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

    private void Start()
    {
        playerData.rigidBody = GetComponent<Rigidbody>();
        playerData.playerCollider = GetComponent<BoxCollider>();
        playerData.playerDashMaterial = GetComponent<Renderer>().material;
        playerData.dashColor = playerData.playerDashMaterial.color;
        playerData.activeMoveSpeed = playerData.speed;
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

        playerData.movementDirection = new Vector2(moveX, moveY).normalized;
        onPlayerWalkChange?.Invoke(isIdle);

        if (playerData.movementDirection.x != 0) 
        {
            isIdle = false;
            onPlayerWalkChange?.Invoke(false);

            playerData.lastHorizontalVector = playerData.movementDirection.x;
            isWalking = true;

            onPlayerWalkChange?.Invoke(isWalking);
        }

        if (playerData.movementDirection.y != 0) 
        {
            isIdle = false;
            onPlayerWalkChange?.Invoke(false);

            playerData.lastVerticalVector = playerData.movementDirection.y;
            isWalking = true;

            onPlayerWalkChange?.Invoke(isWalking);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < 3; i++) 
            {
                selectWeapon[i].PlayerTeleport();
            }
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
}