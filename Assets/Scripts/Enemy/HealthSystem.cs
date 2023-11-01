using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Collider enemyCollider;
    [SerializeField] private Collider enemyTriggerCollider;
    [SerializeField] private GameObject shadow;
    [SerializeField] private float health = 0;
    private ZoneTriggeredEffect _triggerEffect;
    private SpriteRenderer _spriteRenderer;

    public bool _dead;
    public EnemyData enemyData;
    public GameObject firePoint;
    public static int enemyCount;

    [Header("Hit Marker")]
    [SerializeField] private HitMarker hitMarker;

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
                shadow.SetActive(false);

                _triggerEffect.TriggerEffect();

                Invoke("DestroyEnemy", _triggerEffect.dropData.objectLifespan);

                _dead = true;
            }
        }
    }

    public void DestroyEnemy()
    {
        if (gameObject.tag == "Enemy")
        {
            enemyCount--;
        }

        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hitMarker.HitEnemy();
    }
}