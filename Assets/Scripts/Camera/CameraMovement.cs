using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Target to Follow")]
    [SerializeField] private Transform target;
    
    [Header("Camera Offset")]
    [SerializeField] private Vector3 offset;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    /// <summary>
    /// Updates the camera position in the LateUpdate phase to follow the player's target.
    /// </summary>
    private void LateUpdate()
    {
        if (playerData.transform == null)
        {
            Debug.LogWarning("Camera target is not assigned.");
            return;
        }

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        transform.position = desiredPosition;
    }
}