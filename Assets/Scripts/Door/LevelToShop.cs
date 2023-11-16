using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelToShop : MonoBehaviour
{
    [SerializeField] private GameObject teleportText;

    public bool canTeleport = false;
    public bool isPlayerOnTeleportArea = false;

    [Header("Player References")]
    [SerializeField] private GameObject player;

    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOn;
    [SerializeField] private Transitions increaseSizeOff;
    private float timeToWaitTransition = 1f;

    [Header("Teleport Location")]
    [SerializeField] private Transform spawnWeaponSelect;

    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("GameObjects to Deactivate")]
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
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnTeleportArea = true;
            teleportText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnTeleportArea = false;
            teleportText.SetActive(false);
        }
    }

    public void CheckPlayerTeleportToShop() 
    {
        if (isPlayerOnTeleportArea == true) 
        {
            StartCoroutine(increaseSizeOn.ActiveTransition(timeToWaitTransition));
            StartCoroutine(increaseSizeOn.DisableTransition(timeToWaitTransition));
            canTeleport = true;
        }

        StartCoroutine(TeleportToShop(timeToWaitTransition));
    }

    public IEnumerator TeleportToShop(float timeToWait) 
    {
        yield return new WaitForSeconds(timeToWait);

        if (canTeleport == true) 
        {
            player.transform.position = spawnWeaponSelect.transform.position;
            StartCoroutine(increaseSizeOff.ActiveTransition(timeToWaitTransition));
            StartCoroutine(increaseSizeOff.DisableTransition(timeToWaitTransition));
        }

        canTeleport = false;
        isPlayerOnTeleportArea = false;
        basket.SetActive(false);
        teleportText.SetActive(false);
    }
}