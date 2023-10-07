using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Custom/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Spawner")]
    public GameObject model;
    public Animator animator;
    public EnemyDropData dropData;

    public string suhsiName;
    public float damage;
    public string spawnAnimationName;
    public float spawnAnimationDuration;
    public string deathAnimationName;
    public float deathAnimationDuration;

    [Header("AIChase")]

    public float movementSpeed;
    public float avoidanceDistance;

    public bool ifFollowingPlayer;
    public bool isMelee;

    [Header("ShooterEnemy")]

    public float shootDistance;
    public float fireRate;
    public float nextFireTime;
    public float lifeSpawn;
    public float bulletSpeed;
    public GameObject bullet;

    [Header("HealthSystem")]

    public float health;
    public bool isDead;
    public ZoneTriggeredEffect _triggerEffect;
    public SpriteRenderer _spriteRenderer;
}