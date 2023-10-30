using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public WeaponData weaponData;
    public float weaponOverheat;
    public float overheatIncreaseAmount;
    public float overheatDecreaseRate;
    public TMP_Text currentOH;
    public Image weaponBlack;
    public Image weaponRed;

    private float _currentOverheat = 0f;
    private float _timeBetweenShots;
    private bool _canShoot = true;
    private bool _overHeat = false;

    private void Update()
    {
        if (!_overHeat && _canShoot && Input.GetMouseButton(0))
        {
            if (_currentOverheat < weaponOverheat)
            {
                ShootBullet();

                _currentOverheat += overheatIncreaseAmount;

                if (_currentOverheat >= weaponOverheat)
                {
                    StartCoroutine(ShootCooldown());
                }
            }
        }

        ManageOverheat();

        currentOH.text = ((int)_currentOverheat).ToString();//Borrar texto y poner aca el fill
    }

    private void ShootBullet()
    {
        if (weaponData.isShootWeapon)
        {
            Instantiate(weaponData.bulletPrefab, transform.position, transform.rotation);
            _canShoot = false;
            _timeBetweenShots = 1f / weaponData.attackSpeed;
            Invoke(nameof(EnableShooting), _timeBetweenShots);
        }
    }

    private void EnableShooting()
    {
        _canShoot = true;
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
        
        while (_currentOverheat > 0f) 
        {
            yield return null;
        }

        _overHeat = false;
    }
}