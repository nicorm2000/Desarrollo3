using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] private float playerDamage;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float damageCooldown = 0.5f; // Cooldown time in seconds
    [SerializeField] private HealthSystem enemyHealth;


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
                playerHealth.takeDamage(playerDamage);

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

        if (Enemy.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy.GetComponent<HealthSystem>().TakeDamage(enemyDamage);
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