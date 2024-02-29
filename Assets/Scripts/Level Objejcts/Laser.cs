using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Health System Dependencies")]
    [SerializeField] private HealthSystem enemyHealth;
    
    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    /// <summary>
    /// Handles the event when a collider enters the trigger, causing damage to the collider's health system if its layer is included.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            other.GetComponent<HealthSystem>().TakeDamage(playerData.laserDamage);
        }
    }
}