using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlayer : MonoBehaviour
{
    public PlayerData playerData;

    private float currentMaxHealth;

    // Update is called once per frame
    void Update()
    {

    }

    public void UpgradeHealth(float number) 
    {
        
    }

    public void UpgradeSpeed(float number) 
    {
        playerData.speed += number;
        playerData.activeMoveSpeed = playerData.speed;
    }

    public void UpgradeDamage(float number) 
    {
        playerData.weaponData[0].damage += number;
        playerData.weaponData[1].damage += number;
        playerData.weaponData[2].damage += number;
    }
}
