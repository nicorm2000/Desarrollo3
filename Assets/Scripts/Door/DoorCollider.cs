using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    [SerializeField] private NextLevel nextLevel;

    [SerializeField] private GameObject transition;

    private float currentTime = 0.0f;
    private float maxTime = 1f;

    private bool starTime = false;

    private void Update()
    {
        if (starTime == true) 
        {
            currentTime += Time.deltaTime;
        }

        if (currentTime >= maxTime)
        {
            nextLevel.LoadLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            transition.SetActive(true);

            starTime = true;
        }
    }
}