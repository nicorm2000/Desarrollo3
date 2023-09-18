using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    [SerializeField] private NextLevel nextLevel;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject levelSpawn;


    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            player.transform.position = levelSpawn.transform.position;
        }
    }
}
