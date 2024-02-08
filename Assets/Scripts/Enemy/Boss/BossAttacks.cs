using System.Collections;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
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

    private AttackType currentAttack;

    private void Start()
    {
        currentAttack = AttackType.None;
        StartCoroutine(InitialDelayRoutine());
    }

    #region BOSS_PRESENTATION
    private IEnumerator InitialDelayRoutine()
    {
        //Boss entering/spawning animation here
        yield return new WaitForSeconds(bossData.bossSpawningDuration);
        bossPresentation.SetActive(true);
        yield return new WaitForSeconds(bossData.bossPresentationDuration);
        bossPresentation.SetActive(false);
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
            yield return new WaitForSeconds(bossData.attackDelay);
            Debug.Log("Can choose an attacck");
        }
        Debug.Log("Boss is dead");
    }

    private void DetermineNextAttack()
    {
        switch (currentAttack)
        {
            case AttackType.Attack1:
                currentAttack = AttackType.Attack2;
                break;
            case AttackType.Attack2:
                currentAttack = AttackType.Attack3;
                break;
            case AttackType.Attack3:
                currentAttack = AttackType.Attack1;
                break;
            default:
                currentAttack = AttackType.Attack1;
                break;
        }
    }
    #endregion

    private IEnumerator PerformAttack()
    {
        switch (currentAttack)
        {
            case AttackType.Attack1:
                Debug.Log("Attack 1");
                yield return StartCoroutine(choppingTentaclesManager.ChoppingTentaclesCoroutine());
                Debug.Log("Attack 1 finished");
                break;
            case AttackType.Attack2:
                Debug.Log("Attack 2");
                StartCoroutine(inkHellManager.RotateSpawnPoints());
                yield return StartCoroutine(inkHellManager.FireBullets());
                Debug.Log("Attack 2 finished");
                break;
            case AttackType.Attack3:
                Debug.Log("Attack 3");
                yield return StartCoroutine(floorTentacleManager.ActivateFloorTentacleCoroutine(bossData.attack3AmountOfFloorTentacles));
                Debug.Log("Attack 3 finished");
                break;
            default:
                break;
        }
        yield return null;
    }
}