using System;
using System.Threading;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Setup")]
    private ZoneTriggeredEffect _triggerEffect;
    private SpriteRenderer _spriteRenderer;

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

    [Header("Hit Marker")]
    [SerializeField] private HitMarker hitMarker;

    [Header("Timer")]
    [SerializeField] private float maxTime = 0;
    private float timer = 0f;

    private void Start()
    {
        if (gameObject.tag == "Enemy")
        {
            enemyCount++;
        }

        _dead = enemyData.isDead;
        health = enemyData.health;
        _triggerEffect = GetComponent<ZoneTriggeredEffect>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        timer = maxTime;
    }

    private void Update()
    {
        if (health <= 0)
        {
            onEnemyDeadChange?.Invoke(!_dead);
            timer -= Time.deltaTime;

            if (!_dead && timer <= 0)
            {
                enemyCollider.enabled = false;
                enemyTriggerCollider.enabled = false;
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
        _spriteRenderer.enabled = false;
        Invoke("DestroyEnemy", _triggerEffect.dropData.objectLifespan);  
    }
}