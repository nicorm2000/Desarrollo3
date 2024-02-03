using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public enum BossStage
    { 
        WaitingToStart,
        Stage1,
        Stage2,
        Stage3
    }

    [SerializeField] private ColliderTrigger colliderTrigger;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private float enemySpawnInterval;
    [SerializeField] private float enemysMaxAmountAlive;

    private FunctionScheduler scheduler;

    private List<Vector3> spawnPositionList;

    private BossStage bossStage;

    private void Awake()
    {
        scheduler = GetComponent<FunctionScheduler>();

        spawnPositionList = new List<Vector3>();

        foreach (Transform spawnPosition in transform.Find("SpawnPositions"))
        {
            spawnPositionList.Add(spawnPosition.position);
        }

        bossStage = BossStage.WaitingToStart;
    }

    private void Start()
    {
        colliderTrigger.OnPlayerTriggerEnter += ColliderTrigger_OnPlayerTriggerEnter;
    }

    private void OnDestroy()
    {
        scheduler.StopScheduling();
    }

    private void ColliderTrigger_OnPlayerTriggerEnter(object sender, System.EventArgs e)
    {
        StartBossFight();
        colliderTrigger.OnPlayerTriggerEnter -= ColliderTrigger_OnPlayerTriggerEnter;
    }

    private void BossFightState()
    {
        switch (bossStage) 
        {
            default:
            case BossStage.Stage1:
                //if (boss health >= 70%)
                { 
                    StartNextStage();
                }
                break;
            case BossStage.Stage2:
                //if (boss health >= 35%)
                {
                    StartNextStage();
                }
                break;
            case BossStage.Stage3:
                break;
        }
    }

    private void StartNextStage()
    {
        switch (bossStage)
        {
            default:
            case BossStage.WaitingToStart:
                bossStage = BossStage.Stage1;
                    break;
            case BossStage.Stage1:
                bossStage = BossStage.Stage2;
                break;
            case BossStage.Stage2:
                bossStage = BossStage.Stage3;
                break;
        }
    }

    private void StartBossFight()
    {
        Debug.Log("Boss Fight Started!");
        bossStage = BossStage.Stage1;
        StartNextStage();
        SpawnEnemy();
        scheduler.StartScheduling(SpawnEnemy, enemySpawnInterval);
    }

    private void SpawnEnemy()
    {
        //int aliveCount = 0;
        //
        //foreach (EnemySpawn enemySpawned in enemySpawnList)
        //{
        //    if (enemySpawn.IsAlive())
        //    {
        //        aliveCount++;
        //        if (aliveCount >= enemysMaxAmountAlive) 
        //        {
        //            return;
        //        }
        //    }
        //}

        //enemySpawnInterval = enemySpawn;
        //
        //int rnd = Random.Range(0, 100);
        //
        //enemySpawn = enemy;
        //if (rnd < 65) EnemySpawn = enemy2;
        //if (rnd < 15) EnemySpawn = enemy3;

        Vector3 spawnPos = spawnPositionList[Random.Range(0, spawnPositionList.Count)];

        Instantiate(enemy, spawnPos, Quaternion.identity);
    }
}