using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelToShop : MonoBehaviour
{
    [SerializeField] private GameObject teleportText;

    private bool canTeleport = false;
    private bool isPlayerOnTeleportArea = false;

    [Header("Player References")]
    [SerializeField] private GameObject player;

    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOn;
    [SerializeField] private Transitions increaseSizeOff;
    private float timeToWait = 1f;
    private float transitonOffTime = 2f;

    [Header("Teleport Location")]
    [SerializeField] private Transform spawnWeaponSelect;

    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("GameObjects to Deactivate")]
    [SerializeField] private GameObject doorCollider;
    [SerializeField] private GameObject basket;

    private void Start()
    {
        teleportText.SetActive(false);
    }

    /// <summary>
    /// Handles the event when a collider enters the trigger, performs specific actions if the collider's layer is included.
    /// </summary>
    /// <param name="player">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            isPlayerOnTeleportArea = true;
            teleportText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            isPlayerOnTeleportArea = false;
            teleportText.SetActive(false);
        }
    }

    public void CheckPlayerTeleportToShop() 
    {
        if (isPlayerOnTeleportArea == true) 
        {
            StartCoroutine(increaseSizeOn.ActiveTransition(timeToWait));
            StartCoroutine(increaseSizeOn.DisableTransition(timeToWait));
            canTeleport = true;
        }

        StartCoroutine(TeleportToShop(timeToWait));
    }

    public IEnumerator TeleportToShop(float timeToWait) 
    {
        yield return new WaitForSeconds(timeToWait);

        if (canTeleport == true) 
        {
            player.transform.position = spawnWeaponSelect.transform.position;
            StartCoroutine(increaseSizeOff.ActiveTransition(transitonOffTime));
            StartCoroutine(increaseSizeOff.DisableTransition(transitonOffTime));
        }

        doorCollider.SetActive(false);
        basket.SetActive(false);
        teleportText.SetActive(false);
    }
}