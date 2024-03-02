using UnityEngine;

public class BossAnimationStateController : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Animator animator;

    [Header("Boss Attacks Dependencies")]
    [SerializeField] private BossAttacks bossAttacks;

    [Header("Boss Manager Dependencies")]
    [SerializeField] private BossHealthSystem bossHealthSystem;

    private void OnEnable()
    {
        bossAttacks.onBossIdleChange += HandleBossIdleChange;
        bossAttacks.onBossChoppingTentaclesChange += HandleChoppingTentacleChange;
        bossAttacks.onBossInkHellChange += HandleInkHellChange;
        bossAttacks.onBossBlindOctopusChange += HandleBlindOctopusChange;
        bossHealthSystem.onBossDeadChange += HandleBossDeathChange;
    }
    private void OnDisable()
    {
        bossAttacks.onBossIdleChange -= HandleBossIdleChange;
        bossAttacks.onBossChoppingTentaclesChange -= HandleChoppingTentacleChange;
        bossAttacks.onBossInkHellChange -= HandleInkHellChange;
        bossAttacks.onBossBlindOctopusChange -= HandleBlindOctopusChange;
        bossHealthSystem.onBossDeadChange -= HandleBossDeathChange;
    }

    public void HandleBossIdleChangeAfterSpawn()
    {
        HandleBossIdleChange(true);
    }

    private void HandleBossIdleChange(bool isIdle)
    {
        animator.SetBool("IsIdle", isIdle);
    }

    private void HandleChoppingTentacleChange(bool isChoppingTentacle)
    {
        animator.SetBool("IsChoppingTentacles", isChoppingTentacle);
    }

    private void HandleInkHellChange(bool isInkHell)
    {
        animator.SetBool("IsInkHell", isInkHell);
    }

    private void HandleBlindOctopusChange(bool isBlindOctopus) 
    {
        animator.SetBool("IsBlindOctopus", isBlindOctopus);
    }

    private void HandleBossDeathChange(bool isDead) 
    {
        animator.SetBool("IsDead", isDead);
    }
}