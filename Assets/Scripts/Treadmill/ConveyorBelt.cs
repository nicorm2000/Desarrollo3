using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float conveyorBeltSpeed;
    public bool isOnConveyorBelt = false;

    public Rigidbody playerRB;

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
        else if (playerRB != null)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player_ConveyorBelt"))
            {
                isOnConveyorBelt = true;
            }
            playerRB.velocity = transform.right * conveyorBeltSpeed * Time.deltaTime;
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
        else if (playerRB != null)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player_ConveyorBelt"))
            {
                isOnConveyorBelt = false;
            }
            playerRB.velocity = transform.right / conveyorBeltSpeed * Time.deltaTime;
        }
    }
}