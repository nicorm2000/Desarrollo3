using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDoor : MonoBehaviour
{
    [SerializeField] private GameObject nextLevel;

    void Start()
    {
        nextLevel.SetActive(false);
    }
    
    public void ActiveObject() 
    {
        nextLevel.SetActive(true);
    }
}
