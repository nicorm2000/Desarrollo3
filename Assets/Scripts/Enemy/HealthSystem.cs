using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Setup")]
    private ZoneTriggeredEffect _triggerEffect;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private AudioManager _audioManager;
    [SerializeField] private GameObject miniMapIcon;

    public bool _dead;
    public EnemyData enemyData;
    public GameObject firePoint;
    public static int enemyCount;

    public event Action<bool> onEnemyDeadChange;

    [Header("References")]
    [SerializeField] private Collider enemyCollider;
    [SerializeField] private Collider enemyTriggerCollider;
    [SerializeField] private GameObject shadow;
    [SerializeField] private float health = 0;
    [SerializeField] private float objectLifespanOffset;

    [Header("Hit Marker")]
    [SerializeField] private HitMarker hitMarker;

    [Header("Timer")]
    [SerializeField] private float maxTime = 0;
    private float timer = 0f;

    private void Start()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            enemyCount++;
        }

        _dead = enemyData.isDead;
        health = enemyData.health;
        _triggerEffect = GetComponent<ZoneTriggeredEffect>();
        _audioManager = GetComponent<AudioManager>();
        timer = maxTime;
    }

    private void Update()
    {
        if (health <= 0)
        {
            onEnemyDeadChange?.Invoke(!_dead);
            timer -= Time.deltaTime;
            enemyCollider.enabled = false;
            enemyTriggerCollider.enabled = false;
            miniMapIcon.SetActive(false);

            if (!_dead && timer <= 0)
            {
                if (!AudioManager.muteSFX)
                {
                    _audioManager.PlaySound(enemyData.death);
                }
                shadow.SetActive(false);

                _triggerEffect.TriggerEffect();

                _dead = true;

                DestroyEnemyTimer();
            }
        }
    }

    public void DestroyEnemy()
    {
        if (gameObject.tag == "Enemy")
        {
            enemyCount--;
        }

        if(enemyCount <= 0) 
        {
            enemyCount = 0;
        }

        onEnemyDeadChange?.Invoke(false);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hitMarker.HitEnemy();
    }

    private void DestroyEnemyTimer()
    {
        spriteRenderer.enabled = false;
        Invoke("DestroyEnemy", _triggerEffect.dropData.objectLifespan + objectLifespanOffset);
    }
}