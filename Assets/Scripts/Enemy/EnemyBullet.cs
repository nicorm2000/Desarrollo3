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
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        damage = enemyData.damage;
        timer = enemyData.lifeSpawn;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playerData.isDashing && !playerData._isDead) 
            {
                playerHealth.takeDamage(damage);
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet_Collider"))
        {
            Destroy(gameObject);
        }
    }
}
