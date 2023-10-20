using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target; // Player's transform
    [SerializeField] private float cameraFollowSpeed = 5f; // Player's transform
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f); // Default camera offset

    public PlayerData playerData;

    private void FixedUpdate() // Keep the camera in FixedUpdate beacuase if not, the player looks blurry with its movement 
    {
        if (playerData.transform == null)
        {
            Debug.LogWarning("Camera target is not assigned.");
            return;
        }

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.fixedDeltaTime * cameraFollowSpeed); // Smoothly move camera
        transform.position = smoothedPosition;
    }
}