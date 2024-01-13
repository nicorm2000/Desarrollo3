using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Custom/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 movementDirection;

    [Header("Spawner")]
    public GameObject model;
    public Animator animator;
    public EnemyDropData dropData;

    public string sushiName;
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
    public bool canChase = false;

    [Header("ShooterEnemy")]

    public float shootDistance;
    public float fireRate;
    public float lifeSpawn;
    public float bulletSpeed;
    public GameObject bullet;

    [Header("HealthSystem")]

    public float health;
    public bool isDead;
    public ZoneTriggeredEffect _triggerEffect;
    public SpriteRenderer _spriteRenderer;

    [Header("FireDamage")]
    public bool enterEnemy = false;

    [Header("SFX")]
    public string death;
    public string projectile;
    public string attack;

    public void ResetEnemiesValues()
    {
        canChase = false;
    }
}