using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Custom/WeaponData")]

public class WeaponData : ScriptableObject
{
    [Header("Weapon Visuals")]
    public Sprite sprite;
    public GameObject model;

    [Header("SelectWeapon Type")]
    public bool lightWeapon;
    public bool mediumWeapon;
    public bool heavyWeapon;

    [Header("Weapon Attack Type")]
    public bool isMeleeWeapon;
    public bool isShootWeapon;

    [Header("Weapon ID")]
    public int weaponID;

    [Header("Bullet")]
    public float bulletSpeed;
    public float lifespan;
    public float damage;

    [Header("Shoot")]
    public GameObject bulletPrefab;
    public float attackSpeed;

    [Header("Camera Shake Configuration")]
    public float duration;
    public AnimationCurve animationCurve;
}