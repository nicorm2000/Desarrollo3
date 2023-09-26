using UnityEngine;

public class SlipperySurface : MonoBehaviour
{
    [SerializeField] private float slipFactor = 2.0f; // How slippery the surface is (higher values mean more slip)
    [SerializeField] private float speedMultiplier = 1.5f; // Speed multiplier when on the slippery surface

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entity entering the trigger has a Rigidbody2D
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            // Apply speed multiplier to the entity's velocity
            rb.velocity *= speedMultiplier;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Apply slip factor to the entity's velocity while staying on the slippery surface
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Reduce control by increasing slip
            rb.velocity *= slipFactor;
        }
    }
}