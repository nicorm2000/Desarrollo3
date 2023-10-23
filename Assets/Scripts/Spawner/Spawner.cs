using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header ("Spawner")]
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject spawnIndicator;

    public int amountToSpawn;
    public int enemiesToSpawn;

    [Header("Round Counter")]
    [SerializeField] private RoundCounter roundCounter;

    [Header("Shop")]
    [SerializeField] private Shop shop;

    [Header("Spawners Administrator")]
    [SerializeField] private SpawnsAdministrator spawnsAdministrator;

    [Header("ScriptableObjects")]
    public WaveData waveData;
    public EnemyData enemyData;

    private void Start()
    {
        waveData.ResetWavesStacks();
        spawnsAdministrator.EnemiesBySpawnCalculator(waveData.maxEnemies, waveData.spawnsCounter);
        NextRound();
    }

    private void Update()
    {
        if (waveData.currentEnemies <= 0)
        {
            roundCounter.IncreaseRounds(waveData.addRound);
            shop.ActiveShop();
            IncreaseMaxEnemies();
            spawnsAdministrator.EnemiesBySpawnCalculator(waveData.maxEnemies, waveData.spawnsCounter);
            NextRound();
        }
    }

    public IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(spawnTime);

        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject spawnedObject = Instantiate(enemyData.model, GetRandomSpawnPosition(), Quaternion.identity);
            //Here will be the animation for the object to spawn
            enemiesToSpawn--;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        if (spawnPositions.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPositions.Length);
            return spawnPositions[randomIndex].position;
        }
        else
        {
            return transform.position;
        }
    }

    private void NextRound() 
    {
        StartCoroutine(SpawnObjects());
    }

    public void IncreaseMaxEnemies() 
    {
        waveData.maxEnemies = waveData.maxEnemies * 2;
    }
}