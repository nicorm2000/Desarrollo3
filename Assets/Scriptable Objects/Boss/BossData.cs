using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Custom/Boss Data")]
public class BossData : ScriptableObject
{
    [Header("Boss Information")]
    public string sushiName;
    public float bossPresentationDuration;
    public float collisionDamage;
    public float attackDelay;

    [Header("Boss Attack 1 - Phase 1")]
    public string attack1Name;
    public float attack1Damage;
    public float attack1Despawn;
    public float attack1SpawnDelay;
    public float attack1SpawnDelayMultiplier;
    public float attack1SpawnMinimumDelay;
    public Transform[] attack1Objects;

    [Header("Boss Attack 1 - Phase 2")]
    public float p2Attack1Damage;
    public float p2Attack1SpawnDelay;
    public float p2Attack1SpawnDelayMultiplier;
    public float p2Attack1SpawnMinimumDelay;
    public Transform[] p2Attack1Objects;

    [Header("Boss Attack 2")]
    public string attack2Name;
    public float attack2Damage;
    public float attack2Duration;
    public GameObject attack2Object;

    [Header("Boss Attack 3")]
    public string attack3Name;
    public float attack3Damage;
    public float attack3Duration;
    public GameObject attack3Object;

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