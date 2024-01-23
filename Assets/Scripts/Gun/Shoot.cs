using System.Collections;
using UnityEngine;

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

    [Header("ShootLogic")]
    [SerializeField] private GameObject[] gunMuzzle;

    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;

    private void Update()
    {
        if (playerData.isButtonPress)
        {
            StartShoot();
        }
    }

    public void StartShoot()
    {
        if (!gunOverheat._overHeat && gunOverheat._canShoot)
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

        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(weaponData.loaded);
        }
            
        ShootgunShootLogic();

        if (!weaponData.multipleShoots)
        { 
            Instantiate(weaponData.bulletPrefab, transform.position, transform.rotation);
        }

        DisableShooting();
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
        }

        if (gunOverheat._currentOverheat <= 0f)
        {
            gunOverheat._currentOverheat = 0f;
            gunOverheat._canShoot = true;
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

    private void ShootgunShootLogic() 
    {
        if (weaponData.multipleShoots)
        {
            for (int i = 0; i < weaponData.bulletsPerShoot;  i++) 
            {
                Instantiate(weaponData.bulletPrefab, gunMuzzle[i].transform.position, gunMuzzle[i].transform.rotation);
            }         
        }
    }
}