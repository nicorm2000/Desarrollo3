using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ShowPlayerStacks : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;

    [SerializeField] private TMP_Text speedText;

    [SerializeField] private TMP_Text damageText;

    [SerializeField] private Shop shop;
    void Start()
    {
        healthText.text = "Health: " + shop.playerHealth.ToString();
        speedText.text = "Speed: " + shop.playerSpeed.ToString();
        damageText.text = "Damage: " + shop.playerDamage.ToString();
    }

    void Update()
    {
        healthText.text = "Health: " + shop.playerHealth.ToString();
        speedText.text = "Speed: " + shop.playerSpeed.ToString();
        damageText.text = "Damage: " + shop.playerDamage.ToString();
    }
}
