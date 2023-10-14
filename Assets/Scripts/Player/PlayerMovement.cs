using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float dashCounter = 0;
    private float dashCoolDownCounter = 0;

    public PlayerData playerData;
    
    public  SelectWeapon[] selectWeapon;

    public ConveyorBelt conveyorBelt;

    private void Start()
    {
        playerData._rigidBody = GetComponent<Rigidbody>();
        playerData._playerCollider = GetComponent<BoxCollider>();
        playerData._playerDashMaterial = GetComponent<Renderer>().material;
        playerData._originalColor = playerData._playerDashMaterial.color;
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

        if (playerData.movementDirection.x != 0)
            playerData.lastHorizontalVector = playerData.movementDirection.x;

        if (playerData.movementDirection.y != 0)
            playerData.lastVerticalVector = playerData.movementDirection.y;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolDownCounter <= 0 && dashCounter <= 0)
            {
                playerData.isDashing = true;
                playerData._playerDashMaterial.color = Color.cyan;
                playerData.activeMoveSpeed = playerData.dashSpeed;
                dashCounter = playerData.dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0) 
            {
                playerData.isDashing = false;
                playerData.activeMoveSpeed = playerData.speed;
                dashCoolDownCounter = playerData.dashCooldown;
                playerData._playerDashMaterial.color = playerData._originalColor;
            }
        }

        if (dashCoolDownCounter > 0)
        {
            dashCoolDownCounter -= Time.deltaTime;  
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < 3; i++) 
            {
                selectWeapon[i].playerTeleport();
            }
        }
    }

    private void Move()
    {
        if (conveyorBelt.isOnConveyorBelt)
        {
            playerData._rigidBody.velocity = new Vector2((playerData.movementDirection.x * playerData.activeMoveSpeed) + (conveyorBelt.conveyorBeltSpeed * Time.deltaTime), playerData.movementDirection.y * playerData.activeMoveSpeed);
        }
        else
        {
            playerData._rigidBody.velocity = new Vector2(playerData.movementDirection.x * playerData.activeMoveSpeed, playerData.movementDirection.y * playerData.activeMoveSpeed);
        }
    }
}