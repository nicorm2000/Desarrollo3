using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    [SerializeField] private NextLevel nextLevel;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            nextLevel.LoadLevel();
        }
    }
}