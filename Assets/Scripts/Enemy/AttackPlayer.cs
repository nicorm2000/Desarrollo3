using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private PlayerHealth playerHealth;

    public EnemyData enemyData;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider Enemy)
    {
        if (Enemy.gameObject.CompareTag("Player"))
        {
            playerHealth.takeDamage(enemyData.damage);
        }
    }
}