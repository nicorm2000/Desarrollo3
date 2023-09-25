using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float health = 0;
    [SerializeField] private Collider2D enemyCollider2D;
    [SerializeField] private Collider2D enemyTriggerCollider2D;
    private ZoneTriggeredEffect _triggerEffect;
    private SpriteRenderer _spriteRenderer;
    private bool _dead = false;

    private void Start()
    {
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
                enemyCollider2D.enabled = false;
                enemyTriggerCollider2D.enabled = false;

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