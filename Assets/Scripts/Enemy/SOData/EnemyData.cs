using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Custom/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public GameObject model;
    public GameObject bullet;
    public Animator animator;
    public EnemyDropData dropData;

    public string suhsiName;
    public bool isMelee;
    public float damage;
    public string spawnAnimationName;
    public float spawnAnimationDuration;
    public string deathAnimationName;
    public float deathAnimationDuration;

    [Header("AIChase")]

    public float movementSpeed;
    public float avoidanceDistance;

    [Header("HealthSystem")]

    public float health;
    public ZoneTriggeredEffect _triggerEffect;
    public SpriteRenderer _spriteRenderer;
}