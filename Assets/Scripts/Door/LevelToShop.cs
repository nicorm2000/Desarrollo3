using UnityEngine;

public class LevelToShop : MonoBehaviour
{
    [Header("Teleport Location")]
    [SerializeField] private Transform spawnWeaponSelect;

    [Header("GameObjects to Deactivate")]
    [SerializeField] private GameObject doorCollider;
    [SerializeField] private GameObject basket;

    /// <summary>
    /// Handles the event when a collider enters the trigger, performs specific actions if the collider's layer is included.
    /// </summary>
    /// <param name="player">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            player.transform.position = spawnWeaponSelect.transform.position;
            doorCollider.SetActive(false);
            basket.SetActive(false);
        }
    }
}