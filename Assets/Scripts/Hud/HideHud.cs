using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HideHud : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private GameObject hud;


    void Update()
    {
        if (playerHealth.isDead() == true) 
        {
            
        }
    }
}
