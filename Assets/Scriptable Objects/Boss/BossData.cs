using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Custom/Boss Data")]
public class BossData : ScriptableObject
{
    [Header("Boss Information")]
    public string sushiName;
    public float bossSpawningDuration;
    public float bossPresentationDuration;
    public float bossHealthBarShow;
    public float collisionDamage;
    public float attackDelay;

    [Header("Boss Attack 1")]
    public string attack1Name;
    public float attack1Damage;
    public float attack1Despawn;
    public float attack1SpawnDelay;
    public float attack1SpawnDelayMultiplier;
    public float attack1SpawnMinimumDelay;

    [Header("Boss Attack 2")]
    public string attack2Name;
    public float attack2BulletDamage;
    public float attack2BulletSpeed;
    public float attack2BulletLifespan;
    public float attack2BulletSpawnDelay;
    public float attack2BulletSpawnPointRotation;
    public float attack2MaxAmountOfRounds;
    public GameObject attack2Object;

    [Header("Boss Attack 3")]
    public string attack3Name;
    public float attack3Damage;
    public int attack3AmountOfIterations;
    public int attack3AmountOfFloorTentacles;
    public float attack3ActivationInterval;
    public float attack3ActivationDuration;
    public float attack3WarningDisplay;

    [Header("Boss Animations")]
    public float attack1AnimationDuration;
    public float attack2AnimationDuration;
    public float attack3AnimationDuration;

    [Header("Health System")]
    public float health;
    public bool isDead;
    public ZoneTriggeredEffect _triggerEffect;
    public SpriteRenderer _spriteRenderer;

    [Header("SFX")]
    public string spawn;
    public string death;
    public string attack1SFX;
    public string attack2SFX;
    public string attack3SFX;
}