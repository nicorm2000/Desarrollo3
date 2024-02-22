using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Target to Follow")]
    private bool isOnAimPractice = false;

    public Transform target;
    
    [Header("Camera Offset")]
    [SerializeField] public float maxOffset;
    [SerializeField] public Vector3 offset;

    [Header("Camera Movement Speed")]
    [SerializeField] private float cameraSpeed;

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

        CheckCameraGoUp();
        CheckCameraGoDown();

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        transform.position = desiredPosition;
    }

    private void CheckCameraGoUp() 
    {
        if (isOnAimPractice == true)
        {
            if (offset.y >= maxOffset)
            {
                offset.y -= cameraSpeed * Time.deltaTime;
            }
        }
    }

    private void CheckCameraGoDown() 
    {
        if (isOnAimPractice == false)
        {
            if (offset.y < 0)
            {
                offset.y += cameraSpeed * Time.deltaTime;
            }

            if (offset.y >= 0)
            {
                offset.y = 0f;
            }
        }
    }

    public IEnumerator ActiveMoveCameraOffsetY(int timeToWait) 
    {
        isOnAimPractice = true;
        yield return new WaitForSeconds(timeToWait);
    }

    public IEnumerator DesactiveMoveCameraOffsetY(int timeToWait)
    {
        isOnAimPractice = false;
        yield return new WaitForSeconds(timeToWait);
    }
}