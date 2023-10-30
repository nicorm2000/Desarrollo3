using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopWindow;
    [SerializeField] private GameObject hud;
    [SerializeField] private UpgradePlayer upgradePlayer;

    public SpriteCycle[] spriteCycle;
    public PlayerData playerData;

    public void ActivateShop() 
    {
        shopWindow.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void DeactivateShop()
    {
        shopWindow.SetActive(false);
        hud.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void IncreaseHealth()
    {
        if(playerData.healthStackID < playerData.maxLevelStack) 
        {
            playerData.healthStackID += 1f;

            upgradePlayer.UpgradeHealth(20f);

            spriteCycle[0].UpdateStatsUI(playerData.healthStackID);
        }

        else 
        {
            playerData.healthStackID = 5f;
        }
    }

    public void IncreaseSpeed()
    {
        if (playerData.speedStackID < playerData.maxLevelStack)
        {
            playerData.speedStackID += 1f;

            upgradePlayer.UpgradeSpeed(1f);

            spriteCycle[1].UpdateStatsUI(playerData.speedStackID);
        }

        else
        {
            playerData.speedStackID = 5f;
        }
    }

    public void IncreaseDamage()
    {
        if (playerData.damageStackID < playerData.maxLevelStack)
        {
            playerData.damageStackID += 1f;

            upgradePlayer.UpgradeDamage(1f);

            spriteCycle[2].UpdateStatsUI(playerData.damageStackID);
        }
        
        else 
        {
            playerData.damageStackID = 5f;
        }
    }
}