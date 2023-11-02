using UnityEngine;

public class Slower : MonoBehaviour
{
    [Header("Slow Speed")]
    [SerializeField] private float speedMultiplier;

    private AIChase aiChase;
    private AIShooterChase aiShooterChase;

    /// <summary>
    /// Handles the event when a collider enters the trigger.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
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

    /// <summary>
    /// Handles the event when a collider exits the trigger.
    /// </summary>
    /// <param name="other">The collider that exited the trigger.</param>
    private void OnTriggerExit(Collider other)
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