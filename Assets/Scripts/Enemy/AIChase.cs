using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float chaseSpeed;
    public float avoidanceDistance = 1.0f; // Adjust this value as needed

    public PlayerData playerData;

    private void Start()
    {
        playerData.model = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Vector2 playerPosition = playerData.model.transform.position;
        Vector2 currentPosition = transform.position;

        // Calculate the direction towards the player
        Vector2 dirToPlayer = (playerPosition - currentPosition).normalized;

        // Check for collisions in front of the enemy using Raycast
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, dirToPlayer, avoidanceDistance);

        Debug.DrawRay(currentPosition, dirToPlayer, Color.red);

        if (hit.collider != null)
        {
            // If a collision is detected, move away from the obstacle
            Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
            transform.Translate(avoidanceDirection * chaseSpeed * Time.deltaTime);
        }
        else
        {
            // If no collision, move towards the player
            transform.Translate(dirToPlayer * chaseSpeed * Time.deltaTime);
        }
    }
}