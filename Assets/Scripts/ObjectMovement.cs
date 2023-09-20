using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isInsideZone = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has a specific tag
        if (other.CompareTag("Player"))
        {
            // Move the object 5 units in the z-axis
            other.transform.position += new Vector3(0, 0, -5f);

            isInsideZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the colliding object has a specific tag
        if (other.CompareTag("Player"))
        {
            // Move the object back to its original z-axis position
            other.transform.position -= new Vector3(0, 0, -5f);

            isInsideZone = false;
        }
    }
}