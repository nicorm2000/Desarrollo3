using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private float damage;

    private float timer;

    public PlayerData playerData;
    public EnemyData enemyData;

    private void Start()
    {
        playerHealth = EnemyManager.player.GetComponent<PlayerHealth>();
        damage = enemyData.damage;
        timer = enemyData.lifeSpan;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * enemyData.bulletSpeed);

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player_BulletCollider"))
        {
            if (!playerData.isDashing && !playerData._isDead)
            {
                playerHealth.takeDamage(damage);
            }

            Destroy(gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet_Collider"))
        {
            Destroy(gameObject);
        }
    }
}