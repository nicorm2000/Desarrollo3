using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] private float playerDamage;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float damageCooldown = 0.5f;
    [SerializeField] private HealthSystem enemyHealth;

    public PlayerData playerData;
    private PlayerHealth playerHealth;
    private float lastDamageTime;

    private void Start()
    {
        playerData.ResetPlayerFireDamage();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (playerData.enterPlayer && !playerData.isDashing)
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                playerHealth.takeDamage(playerDamage);

                lastDamageTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider Enemy)
    {
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