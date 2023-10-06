using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private float damage;

    public EnemyData enemyData;

    private void Start()
    {
        damage = enemyData.damage;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.CompareTag("Player"))
        {
            playerHealth.takeDamage(damage);
        }
    }
}