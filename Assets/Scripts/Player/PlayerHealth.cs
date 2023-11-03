using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    [Header("Screen Shake Dependencies")]
    [SerializeField] private ScreenShake screenShake;
    
    [Header("Player Health UI Dependencies")]
    [SerializeField] private PlayerHealthUI playerHealthUI;
    [SerializeField] private float borderColorDuration;

    [Header("Scene Manager Dependencies")]
    [SerializeField] private MySceneManager mySceneManager;
    [SerializeField] private string loseScene;

    /// <summary>
    /// Initializes the player's data and updates the player's health UI.
    /// </summary>
    void Start()
    {
        playerData.ResetPlayerStacks();
        playerData.currentHealth = playerData.maxHealth;
        playerHealthUI.SetMaxAndCurrentHealth(playerData.maxHealth, playerData.currentHealth);
    }

    /// <summary>
    /// Inflicts damage to the player, triggers screen shake and UI color change, updates health UI, and handles player death.
    /// </summary>
    /// <param name="damage">The amount of damage to inflict.</param>
    public void takeDamage(float damage) 
    {
        StartCoroutine(screenShake.Shake());
        StartCoroutine(playerHealthUI.ChangeBorderColor(Color.red, borderColorDuration));
        
        playerData.currentHealth -= damage;
        playerHealthUI.SetHealth(playerData.currentHealth);

        if (playerData.currentHealth <= Constants.ZERO_F)
        {
            playerData.currentHealth = Constants.ZERO_F;
            playerData._isDead = true;
            PlayerDies();
        }
        else
        {
            playerData._isDead = false;
        }
    }

    /// <summary>
    /// Handles the actions when the player dies, resets player data, and loads the lose scene.
    /// </summary>
    private void PlayerDies()
    {
        playerData.ResetPlayerFireDamage();
        playerData.ResetPlayerStacks();
        playerData.currentHealth = Constants.ZERO_F;
        mySceneManager.LoadSceneByName(loseScene);
    }
}