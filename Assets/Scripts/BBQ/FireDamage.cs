using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageCooldown = 0.5f; // Cooldown time in seconds

    private PlayerHealth playerHealth;
    private float lastDamageTime; // Record the time of the last damage application
    private bool enterPlayer = false;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (enterPlayer)
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                // Apply damage to the player
                playerHealth.takeDamage(damage);

                // Record the time of the last damage application
                lastDamageTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider Enemy)
    {
        Debug.Log("entro");
        if (Enemy.gameObject.CompareTag("Player"))
        {
            enterPlayer = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enterPlayer = false;
        }
    }
}