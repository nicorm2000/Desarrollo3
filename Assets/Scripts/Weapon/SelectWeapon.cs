using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeapon : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject levelSpawn;
    [SerializeField] private GameObject pickUpWeaponText;

    private void Start()
    {
        pickUpWeaponText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            pickUpWeaponText.SetActive(true);
            
            //player.transform.position = levelSpawn.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            pickUpWeaponText.SetActive(false);

            //player.transform.position = levelSpawn.transform.position;
        }
    }
}
