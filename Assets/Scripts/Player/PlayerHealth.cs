using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private float maxTime = 10f;
    private float timer;

    public event Action<bool> onPlayerDeadChange;

    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOn;
    [SerializeField] private Transitions increaseSizeOff;

    private float transitonOnTime = 1f;
    private float transitonStart = 7f;

    [Header("Camera Shake Configuration")]
    [SerializeField] private ScreenShake screenShake;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve animationCurve;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;
    
    [Header("Player Health UI Dependencies")]
    [SerializeField] private PlayerHealthUI playerHealthUI;

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
        timer = maxTime;
    }

    private void Update()
    {   
        if(playerData._isDead == true)
        {
            timer -= Time.deltaTime;
            PlayerDies();
        }
    }

    /// <summary>
    /// Inflicts damage to the player, triggers screen shake and UI color change, updates health UI, and handles player death.
    /// </summary>
    /// <param name="damage">The amount of damage to inflict.</param>
    public void takeDamage(float damage) 
    {
        StartCoroutine(screenShake.Shake(duration, animationCurve));
        StartCoroutine(playerHealthUI.ChangeBorderColor());
        
        playerData.currentHealth -= damage;
        playerHealthUI.SetHealth(playerData.currentHealth);

        if (playerData.currentHealth <= Constants.ZERO_F)
        {
            playerData.currentHealth = Constants.ZERO_F;
            playerData._isDead = true;
            onPlayerDeadChange?.Invoke(playerData._isDead);
        }
        else
        {
            playerData._isDead = false;
            onPlayerDeadChange?.Invoke(playerData._isDead);
        }
    }

    /// <summary>
    /// Handles the actions when the player dies, resets player data, and loads the lose scene.
    /// </summary>
    private void PlayerDies()
    {
        playerData.ResetPlayerFireDamage();
        playerData.currentHealth = Constants.ZERO_F;

        if (timer <= transitonStart)
        {
            StartCoroutine(increaseSizeOn.ActiveTransition(transitonOnTime));
            StartCoroutine(increaseSizeOn.DisableTransition(transitonOnTime));
        }

        if (timer <= 0) 
        {
            mySceneManager.LoadSceneByName(loseScene);
            playerData.ResetPlayerStacks();
        }
    }
}