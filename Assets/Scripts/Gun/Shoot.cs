using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    [Header("WeaponData")]
    [SerializeField] private WeaponData weaponData;

    [Header("WeaponOverheatUI")]
    [SerializeField] private WeaponOverheatUI weaponOverheatUI;

    [Header("Screen Shake Dependencies")]
    [SerializeField] private ScreenShake screenShake;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    [SerializeField] private GunOverheat gunOverheat;

    private void Update()
    {
        if (playerData._isDead == false)
        {
            StartShoot();
        }
    }

    public void StartShoot()
    {
        if (!gunOverheat._overHeat && gunOverheat._canShoot && Input.GetMouseButton(0))
        {
            if (gunOverheat._currentOverheat < gunOverheat.weaponOverheat)
            {
                ShootLogic();

                gunOverheat.OverheatLogic();
            }
        }
    }

    public void ShootLogic()
    {
        if (weaponData.heavyWeapon)
        {
            StartCoroutine(screenShake.Shake(weaponData.duration, weaponData.animationCurve));
        }

        Instantiate(weaponData.bulletPrefab, transform.position, transform.rotation);
        gunOverheat._canShoot = false;
        gunOverheat._timeBetweenShots = 1f / weaponData.attackSpeed;
        Invoke(nameof(EnableShooting), gunOverheat._timeBetweenShots);
    }

    private void EnableShooting()
    {
        gunOverheat._canShoot = true;
        weaponOverheatUI.currentSliderOverheat = weaponOverheatUI.maxSliderOverheat;
    }

    private void ManageOverheat()
    {
        if (gunOverheat._currentOverheat > 0f)
        {
            gunOverheat._currentOverheat -= gunOverheat.overheatDecreaseRate * Time.deltaTime;

            if (gunOverheat._currentOverheat <= 0f)
            {
                gunOverheat._currentOverheat = 0f;
                gunOverheat._canShoot = true;
            }
        }
    }

    public void DisableShooting()
    {
        gunOverheat._canShoot = false;
        gunOverheat._timeBetweenShots = 1f / weaponData.attackSpeed;
    }

    private IEnumerator ShootCooldown()
    {
        gunOverheat._overHeat = true;
        gunOverheat.overHeatText.SetActive(true);
        gunOverheat.overHeatEffect.SetActive(true);

        while (gunOverheat._currentOverheat > 0f)
        {
            yield return null;
        }

        gunOverheat.overHeatText.SetActive(false);
        gunOverheat.overHeatEffect.SetActive(false);
        gunOverheat._overHeat = false;
    }
}