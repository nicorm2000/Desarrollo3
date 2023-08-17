using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target; // Player's transform
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f); // Default camera offset

    private void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target is not assigned.");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.fixedDeltaTime * 5f); // Smoothly move camera
        transform.position = smoothedPosition;
    }
}