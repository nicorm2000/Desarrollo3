using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunOverheat : MonoBehaviour
{
    [Header("WeaponOverheat")]
    [SerializeField] private WeaponData weaponData;
     public float overheatDecreaseRate;

    public float overheatIncreaseAmount;
    public float weaponOverheat;

    public float _currentOverheat = 0f;
    public bool _canShoot = true;
    public bool _overHeat = false;
    public float _timeBetweenShots;

    [Header("WeaponOverheatUI")]
    [SerializeField] private WeaponOverheatUI weaponOverheatUI;
    [SerializeField] private Image weaponBlack;
    [SerializeField] private Image weaponRed;

    public GameObject overHeatText;
    public GameObject overHeatEffect;

    private void Start()
    {
        weaponOverheatUI.maxSliderOverheat = weaponOverheat;
    }

    private void Update()
    {
        weaponOverheatUI.CheckTypeOfWeapon();
        ManageOverheat();
        weaponOverheatUI.SetCurrentOverheat(_currentOverheat);
    }

    public void OverheatLogic() 
    {
        _currentOverheat += overheatIncreaseAmount;
        weaponOverheatUI.currentSliderOverheat -= 1;

        if (_currentOverheat >= weaponOverheat)
        {
            StartCoroutine(ShootCooldown());
        }
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

    public IEnumerator ShootCooldown()
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
