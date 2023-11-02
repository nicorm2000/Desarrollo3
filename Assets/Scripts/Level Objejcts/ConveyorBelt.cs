using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includePlayerLayer;

    [Header("Conveyor Belt Speeds")]
    [SerializeField] private float _conveyorBeltSpeed;

    private bool _isOnConveyorBelt = false;

    public float conveyorBeltSpeed { get => _conveyorBeltSpeed; set => _conveyorBeltSpeed = value; }
    public bool isOnConveyorBelt { get => _isOnConveyorBelt; set => _isOnConveyorBelt = value; }

    /// <summary>
    /// Handles the event when a collider enters the trigger, applying a conveyor belt effect to the object.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            if (((1 << other.gameObject.layer) & includePlayerLayer) != 0)
            {
                isOnConveyorBelt = true;
            }
            rb.velocity = transform.right * conveyorBeltSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Handles the event when a collider exits the trigger, removing the conveyor belt effect from the object.
    /// </summary>
    /// <param name="other">The collider that exited the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            if (((1 << other.gameObject.layer) & includePlayerLayer) != 0)
            {
                isOnConveyorBelt = false;
            }
            rb.velocity = transform.right / conveyorBeltSpeed * Time.deltaTime;
        }
    }
}