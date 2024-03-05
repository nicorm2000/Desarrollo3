using System;
using System.Collections;
using UnityEngine;

public class BossHealthSystem : MonoBehaviour
{
    public event Action<bool> onBossDeadChange;

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    [Header("Camera Movement")]
    [SerializeField] private CameraMovement cameraMovement;

    [Header("Hit Marker")]
    [SerializeField] private HitMarker hitMarker;

    [Header("Boss Health bar")]
    [SerializeField] private BossHealthBar bossHealthBar;
    [SerializeField] private GameObject goHealthBar;

    [Header("Audio Manager Dependencies")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] private string bossGrunt;
    [SerializeField] private string bossScream;

    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOn;
    private readonly float timeToTurnOnTransition = 5f;

    [Header("Scene Manager Dependencies")]
    [SerializeField] private MySceneManager mySceneManager;
    [SerializeField] private string winScene;

    private bool _isDead;

    private void Start()
    {
        bossData.ResetBossData();
        bossData.currentHealth = bossData.maxHealth;
        bossHealthBar.SetMaxAndCurrentHealth(bossData.maxHealth, bossData.currentHealth);
        _isDead = false;
        onBossDeadChange?.Invoke(_isDead);
    }

    private void Update()
    {
        bossHealthBar.SetHealth(bossData.currentHealth);
    }

    public void TakeDamage(float damage) 
    {
        if (!bossData.isDead) 
        {
            if (!AudioManager.muteSFX && bossData.currentHealth <= 0)
            {
                audioManager.PlaySound(bossGrunt);
            }
            bossData.currentHealth -= damage;
            hitMarker.HitEnemy();
        }

        if(bossData.currentHealth <= 0) 
        {
            _isDead = true;
            onBossDeadChange?.Invoke(_isDead);

            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(bossScream);
            }
            bossData.currentHealth = 0f;
            BossDies();
        }
    }

    public void BossDies() 
    {
        bossData.isDead = true;
        goHealthBar.SetActive(false);

        StartCoroutine(BossDeathSequence());
    }

    private IEnumerator BossDeathSequence()
    {
        yield return new WaitForSeconds(timeToTurnOnTransition);
        StartCoroutine(increaseSizeOn.ActiveTransition(timeToTurnOnTransition));
        _isDead = false;
        mySceneManager.LoadSceneByName(winScene);
    }
}