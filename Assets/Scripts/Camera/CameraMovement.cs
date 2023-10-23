using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraFollowSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

    public PlayerData playerData;

    private void FixedUpdate() // Keep the camera in FixedUpdate beacuase if not, the player looks blurry with its movement 
    {
        if (playerData.transform == null)
        {
            Debug.LogWarning("Camera target is not assigned.");
            return;
        }

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.fixedDeltaTime * cameraFollowSpeed);
        transform.position = smoothedPosition;
    }
}