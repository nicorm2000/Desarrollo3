using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public PlayerData playerData;

    [Header("Dash")]
    public Image dashImage;
    public Color dashColor = Color.cyan;
    private float dashCooldown = 3f;
    private float dashCounter = 0;
    private float dashCoolDownCounter = 0;

    [Header("Shield")]
    public Image shieldImage;
    public KeyCode shield = KeyCode.F2;
    public Color shieldColor = Color.cyan;
    private float shieldCooldown = 3f;
    private bool isCooldownShield = false;

    [Header("Laser")]
    public Image laserImage;
    public KeyCode laser = KeyCode.F3;
    public Color laserColor = Color.cyan;
    private float laserCooldown = 3f;
    private bool isCooldownLaser = false;

    private void Start()
    {
        dashCooldown = playerData.dashCooldown;
        shieldCooldown = playerData.shieldCooldown;
        laserCooldown = playerData.laserCooldown;

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
        if (Input.GetKeyDown(KeyCode.Space) && dashCoolDownCounter <= 0 && dashCounter <= 0)
        {
            dashCoolDownCounter = playerData.dashCooldown;
            dashCounter = playerData.dashLength;
            playerData.isDashing = true;
            playerData.playerDashMaterial.color = dashColor;
            playerData.activeMoveSpeed = playerData.dashSpeed;
            dashImage.fillAmount = 1f;
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                playerData.isDashing = false;
                playerData.activeMoveSpeed = playerData.speed;
                playerData.playerDashMaterial.color = playerData.dashColor;
            }
        }

        if (dashCoolDownCounter > 0)
        {
            dashCoolDownCounter -= Time.deltaTime;
            dashImage.fillAmount = dashCoolDownCounter / playerData.dashCooldown;
        }
    }
}