using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float health = 0;
    public Component componentToKeep;
    private ZoneTriggeredEffect _triggerEffect;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    private bool _dead = false;

    private void Start()
    {
        _triggerEffect = GetComponent<ZoneTriggeredEffect>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (!_dead)
            {
                _spriteRenderer.enabled = false;
                _collider2D.enabled = false;

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