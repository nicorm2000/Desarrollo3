using UnityEngine;

public class PlayerAnimationStateController : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Animator animator;

    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Abilities abilities;
    [SerializeField] private PlayerHealth playerHealth;

    private void OnEnable()
    {
        playerMovement.onPlayerWalkChange += HandlePlayerMovementChange;
        playerMovement.onPlayerIdleChange += HandlePlayerIdleChange;
        abilities.onPlayerDashChange += HandlePlayerDashChange;
        playerHealth.onPlayerDeadChange += HandlePlayerDeadChange;
    }
    private void OnDisable()
    {
        playerMovement.onPlayerWalkChange -= HandlePlayerMovementChange;
        playerMovement.onPlayerIdleChange -= HandlePlayerIdleChange;
        abilities.onPlayerDashChange -= HandlePlayerDashChange;
        playerHealth.onPlayerDeadChange -= HandlePlayerDeadChange;
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
}