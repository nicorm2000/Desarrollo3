using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float damageCooldown = 0.5f; // Cooldown time in seconds

    public PlayerData playerData;
    private PlayerHealth playerHealth;
    private float lastDamageTime; // Record the time of the last damage application

    private void Start()
    {
        playerData.ResetPlayerFireDamage();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (playerData.enterPlayer)
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
            playerData.enterPlayer = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerData.enterPlayer = false;
        }
    }
}