using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject shopWindow;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    [Header("Upgrade Player Dependencies")]
    [SerializeField] private UpgradePlayer upgradePlayer;
    [SerializeField] private float healthUpgradeAmount;

    [Header("Sprite Cycle Dependencies")]
    [SerializeField] private SpriteCycle[] spriteCycle;

    public void ActivatePopUp() 
    {
        shopWindow.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = Constants.ZERO_F;
    }

    public void DeactivatePopUp()
    {
        shopWindow.SetActive(false);
        hud.SetActive(true);
        Time.timeScale = Constants.ONE_F;
    }

    public void IncreaseHealth()
    {
        if(playerData.healthStackID < playerData.maxLevelStack) 
        {
            playerData.healthStackID += Constants.ONE_F;
            upgradePlayer.UpgradeHealth(healthUpgradeAmount);
            spriteCycle[Constants.HEALTH_ID].UpdateStatsUI(playerData.healthStackID);
        }
        else 
        {
            playerData.healthStackID = Constants.MAX_AMOUNT_OF_STACKS;
        }
    }

    public void IncreaseSpeed()
    {
        if (playerData.speedStackID < playerData.maxLevelStack)
        {
            playerData.speedStackID += Constants.ONE_F;
            upgradePlayer.UpgradeSpeed(Constants.ONE_F);
            spriteCycle[Constants.SPEED_ID].UpdateStatsUI(playerData.speedStackID);
        }
        else
        {
            playerData.speedStackID = Constants.MAX_AMOUNT_OF_STACKS;
        }
    }

    public void IncreaseDamage()
    {
        if (playerData.damageStackID < playerData.maxLevelStack)
        {
            playerData.damageStackID += Constants.ONE_F;
            upgradePlayer.UpgradeDamage(Constants.ONE_F);
            spriteCycle[Constants.DAMAGE_ID].UpdateStatsUI(playerData.damageStackID);
        } 
        else 
        {
            playerData.damageStackID = Constants.MAX_AMOUNT_OF_STACKS;
        }
    }
}