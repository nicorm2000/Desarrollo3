using UnityEngine;

public class UpgradePlayer : MonoBehaviour
{
    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;
    
    [Header("Player Health UI Dependencies")]
    [SerializeField] private PlayerHealthUI playerHealthUI;

    /// <summary>
    /// Upgrades the player's health by the specified number, increasing both current and maximum health values.
    /// </summary>
    /// <param name="number">The amount to upgrade the health by.</param>
    public void UpgradeHealth(float number)
    {
        playerData.maxHealth += number;
        playerData.currentHealth += number;

        playerHealthUI.SetMaxAndCurrentHealth(playerData.maxHealth, playerData.currentHealth);
    }

    /// <summary>
    /// Upgrades the player's speed by the specified number.
    /// </summary>
    /// <param name="number">The amount to upgrade the speed by.</param>
    public void UpgradeSpeed(float number)
    {
        playerData.speed += number;
        playerData.activeMoveSpeed = playerData.speed;
    }

    /// <summary>
    /// Upgrades the player's damage for all weapons by the specified number.
    /// </summary>
    /// <param name="number">The amount to upgrade the damage by.</param>
    public void UpgradeDamage(float number)
    {
        playerData.weaponData[0].damage += number;
        playerData.weaponData[1].damage += number;
        playerData.weaponData[2].damage += number;
    }
}