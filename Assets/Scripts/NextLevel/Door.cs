using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject door;

    [SerializeField] private NextLevel nextLevel;

    void Start()
    {
        door.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D player)
    {
        if (player.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Colision");
            nextLevel.LoadLevel();
        }        
    }

    public void ActiveObject()
    {
        door.SetActive(true);
    }
}
