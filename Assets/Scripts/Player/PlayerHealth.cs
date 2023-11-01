using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Image border;
    public PlayerData playerData;
    public ScreenShake screenShake;

    void Start()
    {
        playerData.ResetPlayerStacks();
        playerData.currentHealth = playerData.maxHealth;
        healthBar.SetMaxHealth(playerData.maxHealth);
    }

    public void takeDamage(float damage) 
    {
        StartCoroutine(screenShake.Shake());
        StartCoroutine(ChangeBorderColor(Color.black));
        
        playerData.currentHealth -= damage;
        healthBar.SetHealth(playerData.currentHealth);

        if (playerData.currentHealth <= 0)
        {
            playerData.currentHealth = 0;
        }
    }

    public IEnumerator ChangeBorderColor(Color color)
    {
        border.color = color;
        yield return new WaitForSeconds(3f);

        border.color = Color.white;
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