using UnityEngine;

public class Treadmill : MonoBehaviour
{
    [SerializeField] private float treadmillSpeed = 5f;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = transform.right * treadmillSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = transform.right / treadmillSpeed;
        }
    }
}