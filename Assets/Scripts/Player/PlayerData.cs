using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Custom/PlayerData")]

public class PlayerData : ScriptableObject
{
    public float currentHealth;
    public float maxHealth;
    public float speed;
    public Sprite sprite;
    public GameObject model;
    public WeaponData weaponData;
}
