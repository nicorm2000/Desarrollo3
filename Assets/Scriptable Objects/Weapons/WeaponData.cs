using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Custom/WeaponData")]

public class WeaponData : ScriptableObject
{
    public Sprite sprite;
    public GameObject model;
    public bool isMeleeWeapon;
    public bool isShootWeapon;

    [Header("SelectWeapon")]
    public int weaponID;

    [Header("Bullet")]
    public float bulletSpeed;
    public float lifespan;
    public float damage;

    [Header("Shoot")]
    public GameObject bulletPrefab;
    public float attackSpeed;
}