using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Target to Follow")]
    private bool isOnAimPractice = false;
    public bool isOnBossArena = false;

    private int timeToWait = 1;

    public Transform target;
    public Transform boss;

    [Header("Camera Offset")]
    [SerializeField] public float maxOffsetOnAimPractice;
    [SerializeField] public float maxOffsetOnBossArena;
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

        StartCoroutine(CheckAimPracticeCameraUp());
        StartCoroutine(CheckAimPracticeCameraDown());

        StartCoroutine(CheckBossArenaCameraUp());
        StartCoroutine(CheckBossArenaCameraDown());

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        transform.position = desiredPosition;
    }

    /// <summary>
    /// Check if camera should move up.
    /// </summary>
    private IEnumerator CheckAimPracticeCameraUp() 
    {
        if (isOnAimPractice)
        {
            if (offset.y >= maxOffsetOnAimPractice)
            {
                offset.y -= cameraSpeed * Time.deltaTime;
            }
        }

        yield return new WaitForSeconds(timeToWait);
    }

    /// <summary>
    /// Check if camera should move down.
    /// </summary>
    private IEnumerator CheckAimPracticeCameraDown() 
    {
        if (!isOnAimPractice)
        {
            if (offset.y < 0)
            {
                offset.y += cameraSpeed * Time.deltaTime;
            }
        }

        yield return new WaitForSeconds(timeToWait);
    }

    private IEnumerator CheckBossArenaCameraUp()
    {
        if (isOnBossArena)
        {
            if (offset.y <= maxOffsetOnBossArena)
            {
                offset.y += cameraSpeed * Time.deltaTime;
            }
        }

        yield return new WaitForSeconds(timeToWait);
    }

    private IEnumerator CheckBossArenaCameraDown() 
    {
        if (!isOnBossArena)
        {
            if (offset.y > 0)
            {
                offset.y -= cameraSpeed * Time.deltaTime;
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