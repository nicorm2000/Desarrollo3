using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    public PlayerData playerData;

    private void Start()
    {
        playerData._spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerData.movementDirection.x != 0 || playerData.movementDirection.y != 0)
        {
            SpriteDirectionChecker();
        }
    }

    private void SpriteDirectionChecker()
    {
        if (playerData.lastHorizontalVector < 0)
        {
            playerData._spriteRenderer.flipX = true;
        }

        else
        {
            playerData._spriteRenderer.flipX = false;
        }
    }

    public void FlipPlayerX() 
    {
        playerData.lastHorizontalVector = playerData.movementDirection.x;
    }

    public void FlipPlayerY() 
    {
        playerData.lastVerticalVector = playerData.movementDirection.y;
    }
}