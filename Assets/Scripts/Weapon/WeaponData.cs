using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Custom/WeaponData")]

public class WeaponData : ScriptableObject
{
    public int weaponID;
    public Sprite sprite;
    public GameObject model;
    public GameObject bullet;
    public bool isMeleeWeapon;
    public bool isShootWeapon;
    public float damage;
    public float fireRate;
    public float speedAttack;
}
