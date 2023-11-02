using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;
    
    [Header("Screen Shake Dependencies")]
    [SerializeField] private ScreenShake screenShake;
    
    [Header("Player Health UI Dependencies")]
    [SerializeField] private PlayerHealthUI playerHealthUI;
    [SerializeField] private float borderColorDuration;

    [Header("Lose Scene")]
    [SerializeField] private int loseScene;

    void Start()
    {
        playerData.ResetPlayerStacks();
        playerData.currentHealth = playerData.maxHealth;
        playerHealthUI.SetMaxAndCurrentHealth(playerData.maxHealth, playerData.currentHealth);
    }

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

    private void PlayerDies()
    {
        playerData.ResetPlayerFireDamage();
        playerData.ResetPlayerStacks();
        playerData.currentHealth = Constants.ZERO_F;
        SceneManager.LoadScene(loseScene);
    }
}