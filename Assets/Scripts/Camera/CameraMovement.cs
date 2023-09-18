using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target; // Player's transform
    [SerializeField] private float cameraFollowSpeed = 5f; // Player's transform
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f); // Default camera offset

    private void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target is not assigned.");
            return;
        }

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.fixedDeltaTime * cameraFollowSpeed); // Smoothly move camera
        transform.position = smoothedPosition;
    }
}