using UnityEngine;

public class ClimbableObject2D : MonoBehaviour
{
    public Transform player; // Reference to your 2D character's transform.
    public Collider2D lowestPointCollider; // Reference to the lowest point 2D collider.
    public Collider2D highestPointCollider; // Reference to the highest point 2D collider.
    public float climbSpeed = 1.0f; // Adjust the climb speed.

    private bool isClimbing = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == player)
        {
            player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z + 5f);
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == player)
        {
            isClimbing = false;
        }
    }

    void Update()
    {
        if (isClimbing)
        {
            // Calculate the direction to move along the Z-axis (up or down).
            float zMovement = (transform.position.y < highestPointCollider.transform.position.y) ? 1.0f : 0.0f;

            // Check if the player is below the highest point collider.
            if (transform.position.y < highestPointCollider.transform.position.y)
            {
                // Move the player upwards along the Z-axis.
                player.Translate(Vector3.up * zMovement * climbSpeed * Time.deltaTime);
            }
            else
            {
                // Stop climbing when at the top.
                isClimbing = false;
            }
        }
    }
}