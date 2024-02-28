using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBossUI : MonoBehaviour
{
    [SerializeField] private GameObject bossHealthBar;

    [SerializeField] private CameraMovement cameraMovement;


    void Start()
    {
        bossHealthBar.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bossHealthBar.SetActive(true);
            cameraMovement.BossArenaActivator(true);
        }
    }
}
