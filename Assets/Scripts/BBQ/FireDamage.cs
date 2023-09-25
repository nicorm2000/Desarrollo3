using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageCooldown = 0.5f; // Cooldown time in seconds

    private PlayerHealth playerHealth;
    private float lastDamageTime; // Record the time of the last damage application

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerStay2D(Collider2D Enemy)
    {
        if (Enemy.gameObject.CompareTag("Player"))
        {
            // Check if enough time has passed since the last damage
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                // Apply damage to the player
                playerHealth.takeDamage(damage);

                // Record the time of the last damage application
                lastDamageTime = Time.time;
            }
        }
    }
}