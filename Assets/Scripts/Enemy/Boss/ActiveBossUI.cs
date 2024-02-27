using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBossUI : MonoBehaviour
{
    [SerializeField] private GameObject bossHealthBar;


    void Start()
    {
        bossHealthBar.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bossHealthBar.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bossHealthBar.SetActive(false);
        }
    }

}
