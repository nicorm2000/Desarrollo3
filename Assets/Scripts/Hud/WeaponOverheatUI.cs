using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponOverheatUI : MonoBehaviour
{
    enum Weapons
    {
        Uzi,
        Famas
    }

    [Header("WeaponUI")]
    
    public Slider overheatSlider;

    [SerializeField] private GameObject famasUI;
    [SerializeField] private GameObject uziUI;
    [SerializeField] private WeaponData weaponData;

    [Header("WeaponUI Images")]

    public float currentSliderOverheat = 0f;
    public float maxSliderOverheat;

    public int weaponID;


    void Start()
    {
        currentSliderOverheat = maxSliderOverheat;

        SetMaxOverheat(maxSliderOverheat);
        SetCurrentOverheat(currentSliderOverheat);
    }

    public void SetMaxOverheat(float health)
    {
        overheatSlider.maxValue = health;
        overheatSlider.value = health;
    }

    public void SetCurrentOverheat(float health)
    {
        overheatSlider.value = health;
    }

    public void CheckTypeOfWeapon()
    {
        switch (weaponID)
        {
            case (int)Weapons.Uzi:

                famasUI.SetActive(true);
                uziUI.SetActive(false);

                break;

            case (int)Weapons.Famas:

                famasUI.SetActive(false);
                uziUI.SetActive(true);

                break;
        }
    }
}
