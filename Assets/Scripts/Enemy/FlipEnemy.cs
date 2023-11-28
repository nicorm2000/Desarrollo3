using UnityEngine;

public class FlipEnemy : MonoBehaviour
{
    public EnemyData enemyData;

    public SpriteRenderer enemySprite;

    private void Update()
    {
        if (enemyData.movementDirection.x != 0 || enemyData.movementDirection.y != 0)
        {
            SpriteDirectionChecker();
        }
    }

    private void SpriteDirectionChecker()
    {
        if (enemyData.lastHorizontalVector < 0)
        {
            enemySprite.flipX = true;
        }

        else
        {
            enemySprite.flipX = false;
        }
    }

    public void FlipEnemyX()
    {
        enemyData.lastHorizontalVector = enemyData.movementDirection.x;
    }

    public void FlipEnemyY()
    {
        enemyData.lastVerticalVector = enemyData.movementDirection.y;
    }
}