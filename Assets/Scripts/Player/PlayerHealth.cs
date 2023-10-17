using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    public PlayerData playerData;

    void Start()
    {
        playerData.ResetPlayerStacks();
        playerData.currentHealth = playerData.maxHealth;
        healthBar.SetMaxHealth(playerData.maxHealth);
    }

    public void takeDamage(float damage) 
    {
        playerData.currentHealth -= damage;
        healthBar.SetHealth(playerData.currentHealth);

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