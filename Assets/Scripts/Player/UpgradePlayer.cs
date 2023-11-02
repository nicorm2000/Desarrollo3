using UnityEngine;

public class UpgradePlayer : MonoBehaviour
{
    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;
    
    [Header("Player Health UI Dependencies")]
    [SerializeField] private PlayerHealthUI playerHealthUI;

    public void UpgradeHealth(float number)
    {
        if (playerData.currentHealth == playerData.maxHealth)
        {
            playerData.currentHealth = playerData.maxHealth;
        }
        else
        {
            playerData.currentHealth += number;
        }
        playerData.maxHealth += number;

        playerHealthUI.SetMaxAndCurrentHealth(playerData.maxHealth, playerData.currentHealth);
    }

    public void UpgradeSpeed(float number)
    {
        playerData.speed += number;
        playerData.activeMoveSpeed = playerData.speed;
    }

    public void UpgradeDamage(float number)
    {
        playerData.weaponData[0].damage += number;
        playerData.weaponData[1].damage += number;
        playerData.weaponData[2].damage += number;
    }
}