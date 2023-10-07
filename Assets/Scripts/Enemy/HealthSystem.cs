using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Collider enemyCollider;
    [SerializeField] private Collider enemyTriggerCollider;
    private ZoneTriggeredEffect _triggerEffect;
    private SpriteRenderer _spriteRenderer;
    private float health = 0;

    public bool _dead;
    public EnemyData enemyData;
    public GameObject firePoint;


    private void Start()
    {
        _dead = enemyData.isDead;
        health = enemyData.health;
        _triggerEffect = GetComponent<ZoneTriggeredEffect>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (!_dead)
            {
                _spriteRenderer.enabled = false;
                enemyCollider.enabled = false;
                enemyTriggerCollider.enabled = false;

                _triggerEffect.TriggerEffect();

                Invoke("DestroyEnemy", _triggerEffect.dropData.objectLifespan);

                _dead = true;
            }
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}