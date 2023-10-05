using System.Collections.Generic;
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
    public bool _isDead;
    public GameObject model;
    public WeaponData weaponData;

    [Header("Player Animator")]
    public Animator _animator;
    public SpriteRenderer _spriteRenderer;

    [Header("Player Movement")]
    public float dashSpeed;
    public float dashLength = 0.25f;
    public float dashCooldown = 1;
    public float activeMoveSpeed;
    public float dashCounter;
    public float dashCooldownCounter;
    public Rigidbody _rigidBody;
    public BoxCollider _playerCollider;
    public Material _playerDashMaterial;
    public Color _originalColor;

    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 movementDirection;

    [Header("Shop")]
    public float healthStackID;
    public float speedStackID;
    public float damageStackID;
}
