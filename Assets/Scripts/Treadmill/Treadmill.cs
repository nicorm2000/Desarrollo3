using UnityEngine;

public class Treadmill : MonoBehaviour
{
    [SerializeField] private float treadmillSpeed = 5f;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Debug.Log("gameobject: " + other.gameObject.name);
            rb.velocity = transform.right * treadmillSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = transform.right / treadmillSpeed * Time.deltaTime;
        }
    }
}