using UnityEngine;

public class EnemyAnimationsStateController : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Animator animator;

    [Header("References")]
    [SerializeField] private AIChase aiChase;
    [SerializeField] private AIShooterChase aiShooterChase;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private EnemyExploder enemyExploder;

    private void OnEnable()
    {
        aiChase.onEnemyWalkChange += HandleEnemyMovementChange;
        aiShooterChase.onShooterEnemyWalkChange += HandleShooterEnemyMovement;
        healthSystem.onEnemyDeadChange += HandleEnemyDeadChange;
        enemyExploder.onEnemyAttackChange += HandleEnemyAttackChange;
    }

    private void HandleEnemyMovementChange(bool isWalkin) 
    {
        animator.SetBool("IsWalking", isWalkin);
    }

    private void HandleShooterEnemyMovement(bool isWalkin) 
    {
        animator.SetBool("IsWalking", isWalkin);
    }

    private void HandleEnemyDeadChange(bool isDead) 
    {
        animator.SetBool("IsDead", isDead);
    }

    private void HandleEnemyAttackChange(bool isAttack)
    {
        animator.SetBool("IsAttacking", isAttack);
    }

    private void OnDisable()
    {
        aiChase.onEnemyWalkChange -= HandleEnemyMovementChange;
        aiShooterChase.onShooterEnemyWalkChange -= HandleShooterEnemyMovement;
        healthSystem.onEnemyDeadChange -= HandleEnemyDeadChange;
        enemyExploder.onEnemyAttackChange -= HandleEnemyAttackChange;
    }
}
