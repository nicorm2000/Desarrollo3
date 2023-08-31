using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopWindow;

    [SerializeField] private GameObject hud;

    public float playerHealth = 1.0f;

    public float playerSpeed = 1.0f;

    public float playerDamage = 1.0f;

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
        playerHealth += 1f;
    }

    public void IncreaseSpeed()
    {
        playerSpeed += 1f;
    }

    public void IncreaseDamage()
    {
        playerDamage += 1f;
    }
}