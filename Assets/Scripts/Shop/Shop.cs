using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject shopWindow;
    [SerializeField] private GameObject[] upgradeButtons;
    [SerializeField] private GameObject[] maxTexts;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;
    private float damageToIncease = 5f;

    [Header("Upgrade Player Dependencies")]
    [SerializeField] private UpgradePlayer upgradePlayer;
    [SerializeField] private float healthUpgradeAmount;

    [Header("Sprite Cycle Dependencies")]
    [SerializeField] private SpriteCycle[] spriteCycle;

    [Header("Cursor Object Dependencies")]
    [SerializeField] private CursorObject cursorObject;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string click;

    public bool isPopUpActive = false;

    /// <summary>
    /// Activates the shop pop-up, deactivates the HUD, and freezes the game time.
    /// </summary>
    public void ActivatePopUp() 
    {
        isPopUpActive = true;
        shopWindow.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = Constants.ZERO_F;
    }

    /// <summary>
    /// Deactivates the shop pop-up, activates the HUD, and resumes the game time.
    /// </summary>
    public void DeactivatePopUp()
    {
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(click);
        }

        isPopUpActive = false;
        cursorObject.MouseNotInteract();
        shopWindow.SetActive(false);
        hud.SetActive(true);

        Time.timeScale = Constants.ONE_F;
    }

    /// <summary>
    /// Increases the player's health if the health stack ID is below the maximum level stack.
    /// </summary>
    public void IncreaseHealth()
    {
        if(playerData.healthStackID < playerData.maxLevelStack) 
        {
            playerData.healthStackID += Constants.ONE_F;
            upgradePlayer.UpgradeHealth(healthUpgradeAmount);
            spriteCycle[Constants.HEALTH_ID].UpdateStatsUI(playerData.healthStackID);
        }

        if (playerData.healthStackID >= playerData.maxLevelStack)
        {
            playerData.healthStackID = Constants.MAX_AMOUNT_OF_STACKS;
            upgradeButtons[1].SetActive(false);
            maxTexts[1].SetActive(true);
        }
    }

    /// <summary>
    /// Increases the player's speed if the speed stack ID is below the maximum level stack.
    /// </summary>
    public void IncreaseSpeed()
    {
        if (playerData.speedStackID < playerData.maxLevelStack)
        {
            playerData.speedStackID += Constants.ONE_F;
            upgradePlayer.UpgradeSpeed(Constants.ONE_F);
            spriteCycle[Constants.SPEED_ID].UpdateStatsUI(playerData.speedStackID);
        }

        if (playerData.speedStackID >= playerData.maxLevelStack)
        {
            playerData.speedStackID = Constants.MAX_AMOUNT_OF_STACKS;
            upgradeButtons[0].SetActive(false);
            maxTexts[0].SetActive(true);
        }
    }

    /// <summary>
    /// Increases the player's damage if the damage stack ID is below the maximum level stack.
    /// </summary>
    public void IncreaseDamage()
    {
        if (playerData.damageStackID < playerData.maxLevelStack)
        {
            playerData.damageStackID += Constants.ONE_F;
            upgradePlayer.UpgradeDamage(damageToIncease);
            spriteCycle[Constants.DAMAGE_ID].UpdateStatsUI(playerData.damageStackID);
        }

        if (playerData.damageStackID >= playerData.maxLevelStack)
        {
            playerData.damageStackID = Constants.MAX_AMOUNT_OF_STACKS;
            upgradeButtons[2].SetActive(false);
            maxTexts[2].SetActive(true);
        }
    }
}