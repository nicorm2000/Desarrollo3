using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Target to Follow")]
    [SerializeField] private Transform target;
    
    [Header("Camera Follow Speed")]
    [SerializeField] private float cameraFollowSpeed;
    
    [Header("Camera Offset")]
    [SerializeField] private Vector3 offset;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    private void FixedUpdate() //Keep the camera in FixedUpdate beacuase if not, the player looks blurry with its movement 
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