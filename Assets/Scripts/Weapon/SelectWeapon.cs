using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject levelSpawn;
    [SerializeField] private GameObject pickUpWeaponText;

    [SerializeField] private ChangeWeaponSprite changeWeaponSprite;
    public int weaponNumber;
 
    public bool playerCanTeleport = false;

    private void Start()
    {
        pickUpWeaponText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            playerCanTeleport = true;
            pickUpWeaponText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D player)
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
            changeWeaponSprite.ChangeSprite(weaponNumber);
            player.transform.position = levelSpawn.transform.position;
        }
    }
}
