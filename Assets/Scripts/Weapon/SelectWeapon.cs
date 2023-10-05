using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    [SerializeField] private GameObject levelSpawn;
    [SerializeField] private GameObject pickUpWeaponText;
    [SerializeField] private ChangeWeaponSprite changeWeaponSprite;

    public PlayerData playerData;
    public WeaponData weaponData;
 
    public bool playerCanTeleport = false;

    private void Start()
    {
        pickUpWeaponText.SetActive(false);
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            playerCanTeleport = true;
            pickUpWeaponText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            playerCanTeleport = false;
            pickUpWeaponText.SetActive(false);
        }
    }

    public void playerTeleport() 
    {
        if(playerCanTeleport == true) 
        {
            changeWeaponSprite.ChangeSprite(weaponData.weaponID);
            playerData.model.transform.position = levelSpawn.transform.position;
        }
    }
}
