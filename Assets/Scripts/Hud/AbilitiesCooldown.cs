using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesCooldown : MonoBehaviour
{
    [Header("Dash")]
    public Image dashImage;
    public float dashCooldown = 3f;
    public KeyCode dash = KeyCode.F1;
    private bool isCooldownDash = false;

    [Header("Shield")]
    public Image shieldImage;
    public float shieldCooldown = 3f;
    public KeyCode shield = KeyCode.F2;
    private bool isCooldownShield = false;

    [Header("Laser")]
    public Image laserImage;
    public float laserCooldown = 3f;
    public KeyCode laser = KeyCode.F3;
    private bool isCooldownLaser = false;

    private void Start()
    {
        dashImage.fillAmount = 0f;
        shieldImage.fillAmount = 0f;
        laserImage.fillAmount = 0f;
    }

    private void Update()
    {
        Dash();
        Shield();
        Laser();
    }

    private void Laser()
    {
        if (Input.GetKeyDown(laser) && !isCooldownLaser)
        {
            isCooldownLaser = true;
            laserImage.fillAmount = 1f;
        }

        if (isCooldownLaser)
        {
            laserImage.fillAmount -= 1 / laserCooldown * Time.deltaTime;

            if (laserImage.fillAmount <= 0f)
            {
                laserImage.fillAmount = 0f;

                isCooldownLaser = false;
            }
        }
    }

    private void Shield()
    {
        if (Input.GetKeyDown(shield) && !isCooldownShield)
        {
            isCooldownShield = true;
            shieldImage.fillAmount = 1f;
        }

        if (isCooldownShield)
        {
            shieldImage.fillAmount -= 1 / shieldCooldown * Time.deltaTime;

            if (shieldImage.fillAmount <= 0f)
            {
                shieldImage.fillAmount = 0f;

                isCooldownShield = false;
            }
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(dash) && !isCooldownDash)
        {
            isCooldownDash = true;
            dashImage.fillAmount = 1f;
        }

        if (isCooldownDash)
        {
            dashImage.fillAmount -= 1 / dashCooldown * Time.deltaTime;

            if (dashImage.fillAmount <= 0f )
            {
                dashImage.fillAmount = 0f;

                isCooldownDash = false;
            }
        }
    }
}