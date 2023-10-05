using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopWindow;

    [SerializeField] private GameObject hud;

    public PlayerData playerData;

    public void ActiveShop() 
    {
        shopWindow.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void DesactiveShop()
    {
        shopWindow.SetActive(false);
        hud.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void IncreaseHealth()
    {
        playerData.healthStackID += 1f;
    }

    public void IncreaseSpeed()
    {
        playerData.speedStackID += 1f;
    }

    public void IncreaseDamage()
    {
        playerData.damageStackID += 1f;
    }
}