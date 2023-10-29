using UnityEngine;

[System.Serializable]
public class Wave
{
    public short waveIndex;
    public short numberOfEnemies;
    public GameObject[] enemyType;
    public float spawnInterval;
}

public class WaveManager : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;

    private Wave _currentWave;
    private int _currentWaveIndex;
    private float _nextSpawnTime;
    private bool _canSpawn = true;

    private void Update()
    {
        _currentWave = waves[_currentWaveIndex];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (totalEnemies.Length == 0 && !_canSpawn && _currentWaveIndex + 1 != waves.Length)
        {
            SpawnNextWave();
        }
    }

    private void SpawnNextWave()
    {
        _currentWaveIndex++;
        _canSpawn = true;
    }

    private void SpawnWave()
    {
        if (_canSpawn && _nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = _currentWave.enemyType[Random.Range(0, _currentWave.enemyType.Length)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);
            
            _currentWave.numberOfEnemies--;
            _nextSpawnTime = Time.time + _currentWave.spawnInterval;

            if (_currentWave.numberOfEnemies == 0)
            {
                _canSpawn = false;
            }
        }
    }
}