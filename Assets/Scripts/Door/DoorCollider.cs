using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    [SerializeField] private RoundCounter roundCounter;
    [SerializeField] private GameObject spawnWeaponSelect;
    [SerializeField] private GameObject doorCollider;
    [SerializeField] private GameObject basket;

    public PlayerData playerData;

    private void Start()
    {
        playerData.model = GameObject.FindGameObjectWithTag("Player");
        roundCounter = FindObjectOfType<RoundCounter>();
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            player.transform.position = spawnWeaponSelect.transform.position;
            doorCollider.SetActive(false);
            basket.SetActive(false);

            roundCounter.maxRounds += 5;
        }
    }
}