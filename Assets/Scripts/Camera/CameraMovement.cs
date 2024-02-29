using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Target to Follow")]
    private bool isOnAimPractice = false;
    private bool isOnBossArena = false;

    private int timeToWait = 1;

    public Transform target;
    public Transform boss;

    [Header("Camera Offset: Aim Practice")]
    [SerializeField] public float maxOffsetYOnAimPractice;
    
    [Header("Camera Offset: Boss Arena")]
    [SerializeField] public float maxOffsetYOnBossArena;
    [SerializeField] public float maxOffsetZOnBossArena;

    [Header("Current Camera Offset:")]
    [SerializeField] public Vector3 offset;

    [Header("Camera Movement Speed: Aim Practice")]
    [SerializeField] private float cameraSpeedAP;

    [Header("Camera Movement Speed: Boss Arena")]
    [SerializeField] private float cameraSpeedBA;

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

        StartCoroutine(CheckCameraMovementUp());
        StartCoroutine(CheckCameraMovementDown());

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z);
        transform.position = desiredPosition;
    }

    /// <summary>
    /// Check if camera should move up.
    /// </summary>
    private IEnumerator CheckCameraMovementUp()
    {
        if (isOnBossArena)
        {
            if (offset.y >= maxOffsetYOnBossArena)
            {
                offset.y -= cameraSpeedBA * Time.deltaTime;
            }

            if (offset.z >= maxOffsetZOnBossArena) 
            {
                offset.z -= cameraSpeedBA * Time.deltaTime;
            }

            target = boss;
        }

        if (isOnAimPractice)
        {
            if (offset.y >= maxOffsetYOnAimPractice)
            {
                offset.y -= cameraSpeedAP * Time.deltaTime;
            }
        }

        yield return new WaitForSeconds(timeToWait);
    }

    /// <summary>
    /// Check if camera should move down.
    /// </summary>
    private IEnumerator CheckCameraMovementDown() 
    {
        if (!isOnAimPractice && !isOnBossArena)
        {
            if (offset.y < 0)
            {
                offset.y += cameraSpeedAP * Time.deltaTime;
            }
        }

        yield return new WaitForSeconds(timeToWait);
    }

    /// <summary>
    /// Sets true or false depending on whether the player is at aim practice.
    /// </summary>
    /// <param name="isActive"></param>
    public void AimPractiveActivator(bool isActive) 
    {
        isOnAimPractice = isActive;
    }

    /// <summary>
    /// Sets true or false depending on whether the player is on boss arena.
    /// </summary>
    /// <param name="isActive"></param>
    public void BossArenaActivator(bool isActive) 
    {
        isOnBossArena = isActive;
    }
}