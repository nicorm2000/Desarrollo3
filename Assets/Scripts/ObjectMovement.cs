using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has a specific tag
        if (other.CompareTag("Player"))
        {
            // Move the object 5 units in the z-axis
            other.transform.position += new Vector3(0, 0, -5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the colliding object has a specific tag
        if (other.CompareTag("Player"))
        {
            // Move the object back to its original z-axis position
            other.transform.position -= new Vector3(0, 0, -5f);
        }
    }
}