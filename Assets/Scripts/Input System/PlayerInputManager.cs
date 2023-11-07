using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Shoot playerShoot;
    [SerializeField] private SelectWeapon[] selectWeapon;


    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;

    public void OnMove(InputValue value)
    {
        playerMovement.Movement(value);
    }

    public void OnInteract() 
    {
        for (int i = 0; i < 3; i++)
        {
            selectWeapon[i].PlayerTeleport();
        }
    }
}
