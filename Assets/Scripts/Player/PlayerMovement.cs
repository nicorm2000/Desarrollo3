using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData playerData;
    
    public  SelectWeapon[] selectWeapon;

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
            if (playerData.dashCooldownCounter <= 0 && playerData.dashCounter <= 0)
            {
                playerData._playerCollider.enabled = false;
                playerData._playerDashMaterial.color = Color.cyan;
                playerData.activeMoveSpeed = playerData.dashSpeed;
                playerData.dashCounter = playerData.dashLength;
            }
        }

        if (playerData.dashCounter > 0)
        {
            playerData.dashCounter -= Time.deltaTime;

            if (playerData.dashCounter <= 0) 
            {
                playerData.activeMoveSpeed = playerData.speed;
                playerData.dashCooldownCounter = playerData.dashCooldown;
                playerData._playerCollider.enabled = true;
                playerData._playerDashMaterial.color = playerData._originalColor;
            }
        }

        if (playerData.dashCooldownCounter > 0)
        {
            playerData.dashCooldownCounter -= Time.deltaTime;  
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
        playerData._rigidBody.velocity = new Vector2(playerData.movementDirection.x * playerData.activeMoveSpeed, playerData.movementDirection.y * playerData.activeMoveSpeed);
    }
}