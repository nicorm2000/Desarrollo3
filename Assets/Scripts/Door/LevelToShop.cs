using UnityEngine;

public class LevelToShop : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private GameObject player;

    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOn;
    private float timeToWait = 1f;

    [Header("Teleport Location")]
    [SerializeField] private Transform spawnWeaponSelect;

    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("GameObjects to Deactivate")]
    [SerializeField] private GameObject doorCollider;
    [SerializeField] private GameObject basket;

    /// <summary>
    /// Handles the event when a collider enters the trigger, performs specific actions if the collider's layer is included.
    /// </summary>
    /// <param name="player">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            StartCoroutine(increaseSizeOn.ActiveTransition(timeToWait));
            StartCoroutine(increaseSizeOn.DisableTransition(timeToWait));
            Invoke("TeleportToShop", 1f);
        }
    }

    private void TeleportToShop() 
    {
        player.transform.position = spawnWeaponSelect.transform.position;
        doorCollider.SetActive(false);
        basket.SetActive(false);
    }
}