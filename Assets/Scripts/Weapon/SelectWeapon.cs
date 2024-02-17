using System.Collections;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    [Header("Visual Dependencies")]
    [SerializeField] private GameObject pickUpWeaponText;
    [SerializeField] private ChangePlayerWeapon changePlayerWeapon;

    [SerializeField] private bool canPickUp = false;


    [Header("WeaponData Dependences")]
    [SerializeField] private WeaponData weaponData;

    private void Start()
    {
        pickUpWeaponText.SetActive(false);
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            pickUpWeaponText.SetActive(true);
            canPickUp = true;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            pickUpWeaponText.SetActive(false);
            canPickUp = false;
        }
    }

    public void CheckWeapon() 
    {
        if (canPickUp) 
        {
            changePlayerWeapon.ChangeWeapon(weaponData.weaponID);
        }
    }
}