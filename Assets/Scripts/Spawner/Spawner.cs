using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int amountToSpawn;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject spawnIndicator;

    public WaveData waveData;
    public EnemyData enemyData;

    private void Start()
    {
        amountToSpawn = waveData.maxEnemies;
        waveData.currentEnemies = waveData.maxEnemies;

        StartCoroutine(SpawnObjects());
    }

    public IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(spawnTime);

        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject spawnedObject = Instantiate(enemyData.model, GetRandomSpawnPosition(), Quaternion.identity);
            //Here will be the animation for the object to spawn

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
}