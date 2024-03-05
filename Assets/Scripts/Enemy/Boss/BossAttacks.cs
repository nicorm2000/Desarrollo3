using System;
using System.Collections;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    public event Action<bool> onBossIdleChange;
    public event Action<bool> onBossChoppingTentaclesChange;
    public event Action<bool> onBossInkHellChange;
    public event Action<bool> onBossBlindOctopusChange;

    public enum AttackType
    {
        None,
        Attack1,
        Attack2,
        Attack3
    }

    [Header("Boss UI Set Up")]
    [SerializeField] private GameObject bossPresentation;
    [SerializeField] private GameObject bossHealthBar;

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    [Header("Chopping Tentacles Set Up")]
    [SerializeField] private ChoppingTentaclesManager choppingTentaclesManager;

    [Header("Ink Hell Set Up")]
    [SerializeField] private InkHellManager inkHellManager;

    [Header("Blind Octopus Set Up")]
    [SerializeField] private FloorTentacleManager floorTentacleManager;

    private AttackType _currentAttack;
    private bool _isIdle;
    private bool _isChoppingTentacles;
    private bool _isInkHell;
    private bool _isBlindOctopus;

    [Header("Boss Area")]
    public bool isOnBossArea = false;

    private void Start()
    {
        _currentAttack = AttackType.None;
        StartCoroutine(InitialDelayRoutine());
        isOnBossArea = true;
    }

    #region BOSS_PRESENTATION
    private IEnumerator InitialDelayRoutine()
    {
        yield return new WaitForSeconds(bossData.bossSpawningDuration);
        bossPresentation.SetActive(true);
        yield return new WaitForSeconds(bossData.bossPresentationDuration);
        bossPresentation.SetActive(false);
        _isIdle = true;
        onBossIdleChange?.Invoke(_isIdle);
        bossHealthBar.SetActive(true);
        yield return new WaitForSeconds(bossData.bossHealthBarShow);

        StartCoroutine(AttackCoroutine());
    }
    #endregion

    #region BOSS_ATTACKS
    private IEnumerator AttackCoroutine()
    {
        while (!bossData.isDead)
        {
            DetermineNextAttack();

            yield return StartCoroutine(PerformAttack());
            Debug.Log("Wait for attack delay");
            _isIdle = true;
            onBossIdleChange?.Invoke(_isIdle);
            yield return new WaitForSeconds(bossData.attackDelay);
            Debug.Log("Can choose an attack");
        }
        Debug.Log("Boss is dead");
    }

    private void DetermineNextAttack()
    {
        switch (_currentAttack)
        {
            case AttackType.Attack1:
                _currentAttack = AttackType.Attack2;
                break;
            case AttackType.Attack2:
                _currentAttack = AttackType.Attack3;
                break;
            case AttackType.Attack3:
                _currentAttack = AttackType.Attack1;
                break;
            default:
                _currentAttack = AttackType.Attack1;
                break;
        }
    }
    #endregion

    private IEnumerator PerformAttack()
    {
        switch (_currentAttack)
        {
            case AttackType.Attack1:
                Debug.Log("Attack 1");
                _isIdle = false;
                onBossIdleChange?.Invoke(_isIdle);
                _isChoppingTentacles = true;
                onBossChoppingTentaclesChange?.Invoke(_isChoppingTentacles);
                yield return StartCoroutine(choppingTentaclesManager.ChoppingTentaclesCoroutine());
                _isChoppingTentacles = false;
                onBossChoppingTentaclesChange?.Invoke(_isChoppingTentacles);
                Debug.Log("Attack 1 finished");
                break;
            case AttackType.Attack2:
                Debug.Log("Attack 2");
                _isIdle = false;
                onBossIdleChange?.Invoke(_isIdle);
                _isInkHell = true;
                onBossInkHellChange?.Invoke(_isInkHell);
                yield return StartCoroutine(inkHellManager.FireBullets());
                _isInkHell = false;
                onBossInkHellChange?.Invoke(_isInkHell);
                StartCoroutine(inkHellManager.RotateSpawnPoints());
                Debug.Log("Attack 2 finished");
                break;
            case AttackType.Attack3:
                Debug.Log("Attack 3");
                _isIdle = false;
                onBossIdleChange?.Invoke(_isIdle);
                _isBlindOctopus = true;
                onBossBlindOctopusChange?.Invoke(_isBlindOctopus);
                yield return StartCoroutine(floorTentacleManager.ActivateFloorTentacleCoroutine(bossData.attack3AmountOfFloorTentacles));
                _isBlindOctopus = false;
                onBossBlindOctopusChange?.Invoke(_isBlindOctopus);
                Debug.Log("Attack 3 finished");
                break;
            default:
                break;
        }
        yield return null;
    }
}