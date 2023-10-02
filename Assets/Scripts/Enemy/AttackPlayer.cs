using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float damage;

    private PlayerHealth playerHealth;

    private void Start()
    {
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