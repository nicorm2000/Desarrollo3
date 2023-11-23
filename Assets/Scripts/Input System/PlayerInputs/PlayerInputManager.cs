using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Refernces")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Shoot playerShoot;
    [SerializeField] private SelectWeapon[] selectWeapon;
    [SerializeField] private LevelToShop levelToShop;
    [SerializeField] private Abilities abilities;
    [SerializeField] private LookAtMouse lookAtMouse;

    [Header("Bao Basket Reference")]
    [SerializeField] private GameObject baoBasket;

    [Header("Player Data")]
    [SerializeField] private PlayerData playerData;

    public Vector2 mousePosition { get; private set; }
    public Vector2 mouseDelta { get; private set; }

    public static bool isRightMouseButtonPressed { get; private set; }

    public static event Action<Vector2> OnRightMouseButtonDown;
    public static event Action<Vector2> OnRightMouseButtonUp;

    public void OnCameraPos(InputValue value)
    {
        mouseDelta = value.Get<Vector2>();
    }

    public void OnCameraDragPress()
    {
        isRightMouseButtonPressed = true;
        OnRightMouseButtonDown?.Invoke(mousePosition);
    }

    public void OnCameraDragNotPress()
    {
        isRightMouseButtonPressed = false;
        OnRightMouseButtonUp?.Invoke(mousePosition);
    }

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
                selectWeapon[i].CheckPlayerTeleportToLevel();
            }

            if (baoBasket.activeSelf == true) 
            {
                levelToShop.CheckPlayerTeleportToShop();
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