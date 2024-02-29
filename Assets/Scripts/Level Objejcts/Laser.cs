using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    /// <summary>
    /// Handles the event when a collider enters the trigger, causing damage to the collider's health system if its layer is included.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(playerData.laserDamage);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            other.GetComponent<BossHealthSystem>().TakeDamage(playerData.laserDamageToBoss);
        }
    }
}