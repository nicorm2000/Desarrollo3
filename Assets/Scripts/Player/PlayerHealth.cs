using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ScreenShake screenShake;
    [SerializeField] private PlayerHealthUI playerHealthUI;

    void Start()
    {
        playerData.ResetPlayerStacks();
        playerData.currentHealth = playerData.maxHealth;
        playerHealthUI.SetMaxAndCurrentHealth(playerData.maxHealth, playerData.currentHealth);
    }

    public void takeDamage(float damage) 
    {
        StartCoroutine(screenShake.Shake());
        StartCoroutine(playerHealthUI.ChangeBorderColor(Color.red));
        
        playerData.currentHealth -= damage;
        playerHealthUI.SetHealth(playerData.currentHealth);

        if (playerData.currentHealth <= 0)
        {
            playerData.currentHealth = 0;
        }
    }

    public bool isDead() 
    {
        if (playerData.currentHealth <= 0) 
        {
            playerData._isDead = true; 
        }

        else
        {
            playerData._isDead = false;
        }

        return playerData._isDead;
    }

    private void Update()
    {
        if (isDead() == true) 
        {
            playerData.ResetPlayerFireDamage();
            playerData.ResetPlayerStacks();
            playerData.currentHealth = 0;
            SceneManager.LoadScene(3);
        }
    }
}