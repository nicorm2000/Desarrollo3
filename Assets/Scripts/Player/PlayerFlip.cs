using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    public PlayerData playerData;

    public SpriteRenderer playerSprite;

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
            playerSprite.flipX = true;
        }

        else
        {
            playerSprite.flipX = false;
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