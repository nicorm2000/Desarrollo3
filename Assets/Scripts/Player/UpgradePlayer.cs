using UnityEngine;

public class UpgradePlayer : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    public PlayerData playerData;

    public void UpgradeHealth(float number) 
    {
        if(playerData.currentHealth == playerData.maxHealth) 
        {
            playerData.maxHealth += number;
            playerData.currentHealth = playerData.maxHealth;
            healthBar.SetMaxHealth(playerData.maxHealth);
            healthBar.SetHealth(playerData.currentHealth);
        }

        else
        {
            playerData.maxHealth += number;
            playerData.currentHealth += number;
            healthBar.SetMaxHealth(playerData.maxHealth);
            healthBar.SetHealth(playerData.currentHealth);
        }
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