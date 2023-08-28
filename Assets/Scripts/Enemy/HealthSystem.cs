using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float health = 0;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}