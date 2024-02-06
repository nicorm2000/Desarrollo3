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

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    [Header("Chopping Tentacles Set Up")]
    [SerializeField] private Transform[] tentacles;

    [Header("Ink Hell Set Up")]
    [SerializeField] private Transform[] bulletSpawnPoints;

    [Header("Blind Octopus Set Up")]
    [SerializeField] private FloorTentacleManager floorTentacleManager;

    private AttackType currentAttack;
    private ObjectPool bulletPool;

    private void Start()
    {
        bulletPool = new ObjectPool(bossData.attack2Object);
        currentAttack = AttackType.None;
        StartCoroutine(InitialDelayRoutine());
    }

    #region BOSS_PRESENTATION
    private IEnumerator InitialDelayRoutine()
    {
        //Boss entering/spawning animation here
        yield return new WaitForSeconds(bossData.bossPresentationDuration);//bossData.spawningAnimationDuration
        bossPresentation.SetActive(true);
        yield return new WaitForSeconds(bossData.bossPresentationDuration);
        bossPresentation.SetActive(false);

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
                yield return StartCoroutine(ChoppingTentaclesCoroutine());
                break;
            case AttackType.Attack2:
                Debug.Log("Attack 2");
                StartCoroutine(RotateSpawnPoints());
                yield return StartCoroutine(FireBullets());
                break;
            case AttackType.Attack3:
                Debug.Log("Attack 3");
                yield return StartCoroutine(floorTentacleManager.ActivateFloorTentacleCoroutine(bossData.attack3AmountOfFloorTentacles));
                break;
            default:
                break;
        }

        yield return null;
    }

    #region CHOPPING_TENTACLES
    private IEnumerator ChoppingTentaclesCoroutine()
    {
        float tentacleSpawnDelayAux = bossData.attack1SpawnDelay;
        int activeTentacleIndex = 0;

        while (activeTentacleIndex < tentacles.Length)
        {
            SlamTentacle(activeTentacleIndex);
            yield return new WaitForSeconds(tentacleSpawnDelayAux);

            activeTentacleIndex++;
            tentacleSpawnDelayAux *= bossData.attack1SpawnDelayMultiplier;
            tentacleSpawnDelayAux = Mathf.Max(tentacleSpawnDelayAux, bossData.attack1SpawnMinimumDelay);
        }
        yield return new WaitForSeconds(bossData.attack1Despawn);
        Debug.Log("Attack 1 Finish");
        DeactivateAllTentacles();
    }

    private void SlamTentacle(int index)
    {
        tentacles[index].gameObject.SetActive(true);
    }

    private void DeactivateAllTentacles()
    {
        foreach (Transform tentacle in tentacles)
        {
            tentacle.gameObject.SetActive(false);
        }
    }
    #endregion

    #region INK_HELL
    private IEnumerator FireBullets()
    {
        int bulletCount = 0;

        while (bulletCount < bossData.attack2MaxAmountOfRounds)
        {
            foreach (Transform spawnPoint in bulletSpawnPoints)
            {
                GameObject bullet = bulletPool.GetPooledObject();
                bullet.GetComponent<BossBullets>().ActivateBullet(spawnPoint.position, spawnPoint.localRotation);
            }
            yield return null;
            bulletCount++;
            yield return new WaitForSeconds(bossData.attack2BulletSpawnDelay);
        }
        Debug.Log("Attack 2 Finish");
        StopCoroutine(RotateSpawnPoints());
    }

    private IEnumerator RotateSpawnPoints()
    {
        while (true)
        {
            foreach (Transform spawnPoint in bulletSpawnPoints)
            {
                spawnPoint.Rotate(Vector3.forward, bossData.attack2BulletSpawnPointRotation);
            }
            yield return null;
        }
    }
    #endregion
}