using System;
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

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    [Header("Chopping Tentacles Set Up")]
    [SerializeField] private Transform[] tentacles;

    [Header("Bullet Hell Attack Set Up")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] bulletSpawnPoints;
    [SerializeField] private float bulletSpawnDelay;

    private AttackType currentAttack;
    private ObjectPool bulletPool;

    private void Start()
    {
        bulletPool = new ObjectPool(bulletPrefab);
        StartCoroutine(FireBullets());
        //currentAttack = AttackType.None;
        //StartCoroutine(InitialDelayRoutine());
    }

    private IEnumerator InitialDelayRoutine()
    {
        yield return new WaitForSeconds(bossData.bossPresentationDuration);

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (!bossData.isDead)
        {
            DetermineNextAttack();

            yield return StartCoroutine(PerformAttack());

            yield return new WaitForSeconds(bossData.attackDelay);
        }
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

    private IEnumerator PerformAttack()
    {
        switch (currentAttack)
        {
            case AttackType.Attack1:
                yield return StartCoroutine(ChoppingTentaclesCoroutine());
                break;
            case AttackType.Attack2:
                Debug.Log("Second Attack");
                break;
            case AttackType.Attack3:
                Debug.Log("Third Attack");
                break;
            default:
                break;
        }
    }

    private IEnumerator ChoppingTentaclesCoroutine()
    {
        float tentacleSpawnDelayAux = bossData.attack1SpawnDelay;
        int activeTentacleIndex = 0;

        while (activeTentacleIndex < bossData.attack1Objects.Length)
        {
            SlamTentacle(activeTentacleIndex);
            yield return new WaitForSeconds(tentacleSpawnDelayAux);

            activeTentacleIndex++;
            tentacleSpawnDelayAux *= bossData.attack1SpawnDelayMultiplier;
            tentacleSpawnDelayAux = Mathf.Max(tentacleSpawnDelayAux, bossData.attack1SpawnMinimumDelay);
        }

        yield return new WaitForSeconds(bossData.attack1Despawn);
        DeactivateAllTentacles();
    }

    private void SlamTentacle(int index)
    {
        bossData.attack1Objects[index].gameObject.SetActive(true);
    }

    private void DeactivateAllTentacles()
    {
        foreach (Transform tentacle in bossData.attack1Objects)
        {
            tentacle.gameObject.SetActive(false);
        }
    }

    private IEnumerator FireBullets()
    {
        while (true)
        {
            foreach (Transform spawnPoint in bulletSpawnPoints)
            {
                GameObject bullet = bulletPool.GetPooledObject();
                bullet.GetComponent<BossBullets>().ActivateBullet(spawnPoint.position);
                yield return new WaitForSeconds(bulletSpawnDelay);
            }
            yield return null;
        }
    }
}