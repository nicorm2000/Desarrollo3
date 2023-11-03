using UnityEngine;

public class LevelToShop : MonoBehaviour
{
    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Teleport Location")]
    [SerializeField] private Transform spawnWeaponSelect;

    [Header("GameObjects to Deactivate")]
    [SerializeField] private GameObject doorCollider;
    [SerializeField] private GameObject basket;

    /// <summary>
    /// Handles the event when a collider enters the trigger, performs specific actions if the collider's layer is included.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & includeLayer) != 0)
        {
            other.transform.position = transform.position;
            doorCollider.SetActive(false);
            basket.SetActive(false);
        }
    }
}