using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Shoot playerShoot;
    [SerializeField] private SelectWeapon[] selectWeapon;
    [SerializeField] private Abilities abilities;

    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;

    public void OnMove(InputValue value)
    {
        playerMovement.Movement(value);
    }

    public void OnInteract() 
    {
        if (playerData._isDead == false)
        {
            for (int i = 0; i < 3; i++)
            {
                selectWeapon[i].PlayerTeleport();
            }
        }
    }

    public void OnDash() 
    {
        if (playerData._isDead == false)
        {
            abilities.DashLogic();
        }
    }

    public void OnSlower()
    {
        if (playerData._isDead == false)
        {
            abilities.SlowerLogic();
        }
    }

    public void OnLaser()
    {
        if (playerData._isDead == false) 
        {
            abilities.LaserLogic();

        }
    }
}