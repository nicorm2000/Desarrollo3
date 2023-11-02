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