using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponOverheatUI : MonoBehaviour
{
    enum Weapons
    {
        Uzi,
        Famas,
        Sniper,
        Pistol,
        Shotgun,
        Rpg
    }

    [Header("WeaponUI")]
    
    public Slider overheatSlider;

    [SerializeField] private GameObject uziUI;
    [SerializeField] private GameObject famasUI;
    [SerializeField] private GameObject sniperUI;
    [SerializeField] private GameObject pistolUI;
    [SerializeField] private GameObject shotgunUI;
    [SerializeField] private GameObject rpgUI;

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

                famasUI.SetActive(false);
                uziUI.SetActive(true);
                sniperUI.SetActive(false);
                pistolUI.SetActive(false);
                shotgunUI.SetActive(false);
                rpgUI.SetActive(false);

                break;

            case (int)Weapons.Famas:

                famasUI.SetActive(true);
                uziUI.SetActive(false);
                sniperUI.SetActive(false);
                pistolUI.SetActive(false);
                shotgunUI.SetActive(false);
                rpgUI.SetActive(false);

                break;

            case (int)Weapons.Sniper:

                famasUI.SetActive(false);
                uziUI.SetActive(false);
                sniperUI.SetActive(true);
                pistolUI.SetActive(false);
                shotgunUI.SetActive(false);
                rpgUI.SetActive(false);

                break;

            case (int)Weapons.Pistol:

                famasUI.SetActive(false);
                uziUI.SetActive(false);
                sniperUI.SetActive(false);
                pistolUI.SetActive(true);
                shotgunUI.SetActive(false);
                rpgUI.SetActive(false);

                break;

            case (int)Weapons.Shotgun:

                famasUI.SetActive(false);
                uziUI.SetActive(false);
                sniperUI.SetActive(false);
                pistolUI.SetActive(false);
                shotgunUI.SetActive(true);
                rpgUI.SetActive(false);

                break;

            case (int)Weapons.Rpg:

                famasUI.SetActive(false);
                uziUI.SetActive(false);
                sniperUI.SetActive(false);
                pistolUI.SetActive(false);
                shotgunUI.SetActive(false);
                rpgUI.SetActive(true);

                break;
        }
    }
}
