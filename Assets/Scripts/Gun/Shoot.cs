using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    [Header("WeaponOverheat")]
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private float weaponOverheat;
    [SerializeField] private float overheatIncreaseAmount;
    [SerializeField] private float overheatDecreaseRate;

    private float _currentOverheat = 0f;
    private float _timeBetweenShots;
    private bool _canShoot = true;
    private bool _overHeat = false;

    [Header("WeaponOverheatUI")]
    [SerializeField] private WeaponOverheatUI weaponOverheatUI;

    [SerializeField] private Image weaponBlack;
    [SerializeField] private Image weaponRed;

    [SerializeField] private GameObject overHeatText;
    [SerializeField] private GameObject overHeatEffect;

    [Header("Screen Shake Dependencies")]
    [SerializeField] private ScreenShake screenShake;

    [SerializeField] private PlayerData playerData;

    private void Start()
    {
        weaponOverheatUI.maxSliderOverheat = weaponOverheat;
    }

    private void Update()
    {
        if(playerData._isDead == false) 
        {
            StartShoot();
        }
    }

    public void StartShoot() 
    {
        if (!_overHeat && _canShoot && Input.GetMouseButton(0))
        {
            if (_currentOverheat < weaponOverheat)
            {
                ShootLogic();

                _currentOverheat += overheatIncreaseAmount;
                weaponOverheatUI.currentSliderOverheat -= 1;

                if (_currentOverheat >= weaponOverheat)
                {
                    StartCoroutine(ShootCooldown());
                }
            }
        }

        weaponOverheatUI.CheckTypeOfWeapon();
        ManageOverheat();
        weaponOverheatUI.SetCurrentOverheat(_currentOverheat);
    }

    public void ShootLogic()
    {
        if (weaponData.heavyWeapon)
        {
            StartCoroutine(screenShake.Shake(weaponData.duration, weaponData.animationCurve));
        }

        Instantiate(weaponData.bulletPrefab, transform.position, transform.rotation);
        _canShoot = false;
        _timeBetweenShots = 1f / weaponData.attackSpeed;
        Invoke(nameof(EnableShooting), _timeBetweenShots);
    }

    private void EnableShooting()
    {
        _canShoot = true;
        weaponOverheatUI.currentSliderOverheat = weaponOverheatUI.maxSliderOverheat;
    }

    private void ManageOverheat()
    {
        if (_currentOverheat > 0f)
        {
            _currentOverheat -= overheatDecreaseRate * Time.deltaTime;

            if (_currentOverheat <= 0f)
            {
                _currentOverheat = 0f;
                _canShoot = true;
            }
        }
    }

    private IEnumerator ShootCooldown()
    {
        _overHeat = true;
        overHeatText.SetActive(true);
        overHeatEffect.SetActive(true);

        while (_currentOverheat > 0f)
        {
            yield return null;
        }

        overHeatText.SetActive(false);
        overHeatEffect.SetActive(false);
        _overHeat = false;
    }
}