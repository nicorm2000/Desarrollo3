using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Shoot playerShoot;
    [SerializeField] private SelectWeapon[] selectWeapon;
    [SerializeField] private Abilities abilities;
    [SerializeField] private LookAtMouse lookAtMouse;

    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;

    public Vector2 mousePosition { get; private set; }

    public void OnMove(InputValue value)
    {
        playerMovement.Movement(value);
    }

    public void OnShootPress()
    {
        if (!playerData._isDead)
        {
            playerData.isButtonPress = true;
        }
    }

    public void OnMousePosition(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
    }

    public void OnShootNotPress()
    {
        playerData.isButtonPress = false;
    }

    public void OnInteract()
    {
        if (playerData._isDead == false)
        {
            for (int i = 0; i < 3; i++)
            {
                selectWeapon[i].CheckPlayerTeleport();
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