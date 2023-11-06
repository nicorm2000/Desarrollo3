using UnityEngine;

public class LevelToShop : MonoBehaviour
{
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
            other.transform.position = spawnWeaponSelect.transform.position;
            doorCollider.SetActive(false);
            basket.SetActive(false);
        }
    }
}