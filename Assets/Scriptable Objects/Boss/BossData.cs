using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Custom/Boss Data")]
public class BossData : ScriptableObject
{
    [Header("Boss Information")]
    public string sushiName;
    public float bossPresentationDuration;
    public float collisionDamage;

    [Header("Boss Attack 1")]
    public string attack1Name;
    public float attack1Damage;
    public float attack1Duration;
    public GameObject attack1Object;

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