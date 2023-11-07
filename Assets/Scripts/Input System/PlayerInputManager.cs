using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Refernces")]
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void OnMove(InputValue value)
    {
        playerMovement.Movement(value);
    }
}
