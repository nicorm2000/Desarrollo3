using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float health = 0;
    private ZoneTriggeredEffect _triggerEffect;
    private bool _dead = false;
    private float _count;

    private void Start()
    {
        _triggerEffect = GetComponent<ZoneTriggeredEffect>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            if (!_dead)
            {
                _count++;

                _triggerEffect.TriggerEffect();
                
                if (_triggerEffect.dropData.objectLifespan <= 0)
                {

                    Destroy(gameObject);
                }
                
                _dead = true;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}