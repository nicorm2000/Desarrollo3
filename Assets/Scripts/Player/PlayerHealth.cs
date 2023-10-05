using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    public PlayerData playerData;


    void Start()
    {
        ResetPlayerStacks();
        playerData.currentHealth = playerData.maxHealth;
        healthBar.SetMaxHealth(playerData.maxHealth);
    }

    public void takeDamage(float damage) 
    {
        playerData.currentHealth -= damage;
        healthBar.SetHealth(playerData.currentHealth);
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
    public void ResetPlayerStacks()
    {
        playerData.healthStackID = 1;
        playerData.speedStackID = 1;
        playerData.damageStackID = 1;
    }

    private void Update()
    {
        if (isDead() == true) 
        {
            ResetPlayerStacks();
            playerData.currentHealth = 0;
            SceneManager.LoadScene(2);
        }
    }
}