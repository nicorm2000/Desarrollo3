using UnityEngine;

public class SlipperySurface : MonoBehaviour
{
    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Speed Multiplier")]
    [SerializeField] private float speedMultiplier = 1.5f;

    private AIChase aiChase;
    private AIShooterChase aiShooterChase;

    /// <summary>
    /// Handles the event when a collider enters the trigger, increasing the chase speed of the AI if its layer is included.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            aiChase = other.GetComponent<AIChase>();
            aiShooterChase = other.GetComponent<AIShooterChase>();

            if (aiChase != null)
            {
                aiChase.chaseSpeed *= speedMultiplier;
            }
            else if (aiShooterChase != null)
            {
                aiShooterChase.chaseSpeed *= speedMultiplier;
            }
        }
    }

    /// <summary>
    /// Handles the event when a collider exits the trigger, decreasing the chase speed of the AI if its layer is included.
    /// </summary>
    /// <param name="other">The collider that exited the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO)
        {
            aiChase = other.GetComponent<AIChase>();
            aiShooterChase = other.GetComponent<AIShooterChase>();

            if (aiChase != null)
            {
                aiChase.chaseSpeed /= speedMultiplier;
            }
            else if (aiShooterChase != null)
            {
                aiShooterChase.chaseSpeed /= speedMultiplier;
            }
        }
    }
}