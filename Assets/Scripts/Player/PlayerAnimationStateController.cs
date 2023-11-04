using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationStateController : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Animator animator;

    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;

    private void OnEnable()
    {
        
    }

    private void HandlePlayerIdleChange(bool isIdle)
    {
        animator.SetBool("IsIdle", isIdle);
    }

    private void HandlePlayerMovementChange(bool isWalking) 
    {
        animator.SetBool("IsWalking", isWalking);
    }

    private void HandlePlayerDashChange(bool isDashing) 
    {
        animator.SetBool("IsDashing", isDashing);
    }

    private void HandlePlayerDeadChange(bool isDead) 
    {
        animator.SetBool("IsDead", isDead);
    }

    private void OnDisable()
    {
        
    }
}
