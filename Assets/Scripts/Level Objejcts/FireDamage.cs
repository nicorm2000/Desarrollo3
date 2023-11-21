using System.Collections;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] private float playerDamage;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float damageCooldown = 0.5f;
    [SerializeField] private HealthSystem enemyHealth;
    [SerializeField] private PlayerHealth playerHealth;

    public PlayerData playerData;
    private bool canDamage = true;

    private void Start()
    {
        playerData.ResetPlayerFireDamage();
    }

    private void Update()
    {
        if (playerData.enterPlayer && !playerData.isDashing)
        {
            if (canDamage && !playerData._isDead)
            {
                StartCoroutine(DealDamageWithCooldown());
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

    private IEnumerator DealDamageWithCooldown()
    {
        canDamage = false;
        playerHealth.takeDamage(playerDamage);
        yield return new WaitForSeconds(damageCooldown);

        if (gameObject.activeSelf)
        {
            canDamage = true;
            playerData.enterPlayer = false;
        }
    }

    private void OnDisable()
    {
        playerData.enterPlayer = false;
    }
}