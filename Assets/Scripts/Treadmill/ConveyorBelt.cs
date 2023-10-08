using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float conveyorBeltSpeed;
    public bool isOnConveyorBelt = false;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                isOnConveyorBelt = true;
            }

            rb.velocity = transform.right * conveyorBeltSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                isOnConveyorBelt = false;
            }

            rb.velocity = transform.right / conveyorBeltSpeed * Time.deltaTime;
        }
    }
}