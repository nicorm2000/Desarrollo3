using UnityEngine;

public class BossBullets : MonoBehaviour
{
    [Header("Boss Bullet Set Up")]
    [SerializeField] private string playerBulletCollisionTag;
    [SerializeField] private string bulletCollision;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;
    
    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    private PlayerHealth _playerHealth;
    private float _damage;
    private float _timer;

    private void Start()
    {
        _playerHealth = EnemyManager.player.GetComponent<PlayerHealth>();
        _damage = bossData.attack2BulletDamage;
        _timer = bossData.attack2BulletLifespan;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * bossData.attack2BulletSpeed * Time.deltaTime);

        _timer -= Time.deltaTime;
        
        if (_timer <= 0f)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerBulletCollisionTag))
        {
            if (!playerData.isDashing && !playerData._isDead)
            {
                _playerHealth.takeDamage(_damage);
            }

            ReturnToPool();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer(bulletCollision))
        {
            ReturnToPool();
        }
    }

    public void ActivateBullet(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.position = position;

        transform.rotation = rotation;

        _timer = bossData.attack2BulletLifespan;
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}