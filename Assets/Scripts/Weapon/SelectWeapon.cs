using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOn;
    [SerializeField] private Transitions increaseSizeOff;

    private float transitonOnTime = 1f;
    private float transitonOffTime = 2f;

    [SerializeField] private GameObject levelSpawn;
    [SerializeField] private GameObject pickUpWeaponText;
    [SerializeField] private ChangePlayerWeapon changePlayerWeapon;
    [SerializeField] private GameObject player;

    public PlayerData playerData;
    public WeaponData weaponData;

    public bool playerCanTeleport = false;
    public bool isPlayerOnTeleportArea = false;

    private void Start()
    {
        pickUpWeaponText.SetActive(false);
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            isPlayerOnTeleportArea = true;
            pickUpWeaponText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            isPlayerOnTeleportArea = false;
            pickUpWeaponText.SetActive(false);
        }
    }

    public void CheckPlayerTeleport()
    {
        playerData.haveAGun = true;

        if (isPlayerOnTeleportArea == true)
        {
            StartCoroutine(increaseSizeOn.ActiveTransition(transitonOnTime));
            StartCoroutine(increaseSizeOn.DisableTransition(transitonOnTime));
            playerCanTeleport = true;
        }

        StartCoroutine(PlayerTeleport(1f));
    }

    public IEnumerator PlayerTeleport(float timeToWait) 
    {
        yield return new WaitForSeconds(timeToWait);

        if (playerCanTeleport == true) 
        {
            changePlayerWeapon.ChangeWeapon(weaponData.weaponID);
            player.transform.position = levelSpawn.transform.position;
        }
        
        playerCanTeleport = true;
        StartCoroutine(increaseSizeOff.ActiveTransition(transitonOffTime));
        StartCoroutine(increaseSizeOff.DisableTransition(transitonOffTime));
    }
}