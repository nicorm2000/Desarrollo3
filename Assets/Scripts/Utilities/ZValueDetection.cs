using UnityEngine;

public class ZValueDetection : MonoBehaviour
{
    [Header("Raycast's Transform")]
    [SerializeField] private Transform raycastTop;
    [SerializeField] private Transform raycastBot;

    [Header("Raycast height")]
    [SerializeField] private float maxHeight;

    [Header("Position Offset")]
    [SerializeField] private float positionOffset;

    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("RayCast Color")]
    [SerializeField] private Color raycastColor;

    /// <summary>
    /// Updates the Z position of the object.
    /// </summary>
    private void Update()
    {
        UpdateZPosition();
    }

    /// <summary>
    /// Performs raycasts to determine the Z position of the object based on the hit points.
    /// </summary>
    private void UpdateZPosition()
    {
        if (Physics.Raycast(raycastBot.position, transform.forward, out RaycastHit hit1, maxHeight, includeLayer) 
            && Physics.Raycast(raycastTop.position, transform.forward, out RaycastHit hit2, maxHeight, includeLayer))
        {
            float value1 = hit1.transform.position.z;
            float value2 = hit2.transform.position.z;

            if (value1 == value2)
            {
                Vector3 newPos = transform.position;
                UpdateZPositionBasedOnPoint(newPos, hit1);
            }
            else
            {
                Vector3 newPos = transform.position;
                if (value1 < value2)
                {
                    UpdateZPositionBasedOnPoint(newPos, hit1);
                }
                else if (value1 > value2)
                {
                    UpdateZPositionBasedOnPoint(newPos, hit2);
                }
            }
        }

        DebugRays(raycastColor);
    }

    /// <summary>
    /// Updates the Z position of the object based on a specific hit point.
    /// </summary>
    /// <param name="pos">The current position of the object.</param>
    /// <param name="value">The RaycastHit containing the hit point information.</param>
    private void UpdateZPositionBasedOnPoint(Vector3 pos, RaycastHit value)
    {
        pos.z = value.point.z - positionOffset;
        transform.position = pos;
    }

    /// <summary>
    /// Draws debug rays with a specified color for visualization purposes.
    /// </summary>
    /// <param name="rayColor">The color of the debug rays.</param>
    private void DebugRays(Color rayColor)
    {
        Debug.DrawRay(raycastTop.position, transform.forward * maxHeight, rayColor);
        Debug.DrawRay(raycastBot.position, transform.forward * maxHeight, rayColor);
    }
}