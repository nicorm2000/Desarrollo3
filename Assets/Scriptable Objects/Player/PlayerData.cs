using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Custom/PlayerData")]

[Serializable]
public class PlayerData : ScriptableObject
{
    [Header("Player Movement")]
    public float currentHealth;
    public float maxHealth;
    public float speed;
    public bool _isDead = false;
    public WeaponData[] weaponData;

    [Header("Player Invulnerability")]
    public float invulnerabilityTime;

    [Header("Player Animator")]
    public Animator _animator;
    public SpriteRenderer _spriteRenderer;

    [Header("Player Dash")]
    public float dashCooldown = 3;
    public float dashSpeed;
    public float dashLength = 0.25f;
    public float activeMoveSpeed;
    public bool isDashing;
    public Rigidbody rigidBody;
    public BoxCollider playerCollider;
    public Material playerDashMaterial;
    public Color dashColor;

    [Header("Player Shield")]
    public float shieldCooldown;
    public float shieldDuration;
    public Color shieldColor;

    [Header("Player Laser")]
    public float laserCooldown;
    public float laserDamage;
    public float laserRange;
    public float laserWidth;
    public float laserDuration;
    public Color laserColor;

    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 movementDirection;

    [Header("Shop")]
    public float maxLevelStack;
    public float healthStackID = 0;
    public float speedStackID = 0;
    public float damageStackID = 0;

    [Header("FireDamage")]
    public bool enterPlayer = false;

    [Header("Look At Mouse")]
    public Transform transform;

    [Header("Shoot")]
    public bool haveAGun;
    public bool isButtonPress;

    [Header("SFX")]
    public string death;
    public string damageHit;
    public string shopFall;
    public string pickUpWeapon;

    public void ResetPlayerStacks() 
    {
        _isDead = false;
        enterPlayer = false;
        haveAGun = false;
        healthStackID = 0;
        speedStackID = 0;
        damageStackID = 0;

        speed = 5;
        maxHealth = 100;
        weaponData[0].damage = 35;
        weaponData[1].damage = 250;
        weaponData[2].damage = 15;
    }

    public void ResetPlayerFireDamage() 
    {
        enterPlayer = false;
    }
}