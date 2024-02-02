using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Custom/Boss Data")]
public class BossData : ScriptableObject
{
    [Header("Boss Information")]
    public string sushiName;

    [Header("HealthSystem")]
    public float health;
    public bool isDead;
    public ZoneTriggeredEffect _triggerEffect;
    public SpriteRenderer _spriteRenderer;

    [Header("SFX")]
    public string death;
    public string projectile;
    public string attack;
}