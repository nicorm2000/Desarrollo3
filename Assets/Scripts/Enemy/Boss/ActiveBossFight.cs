using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBossFight : MonoBehaviour
{
    [Header("Boss Dependeces")]
    [SerializeField] private GameObject bossArena;

    [Header("Camera Movement Dependences")]
    [SerializeField] private CameraMovement cameraMovement;

    private void Start()
    {
        bossArena.SetActive(false);
    }

    public IEnumerator ActiveBoss(float timeToWait)
    {
        cameraMovement.BossArenaActivator(true);
        bossArena.SetActive(true);

        yield return new WaitForSeconds(timeToWait);
    }
}
