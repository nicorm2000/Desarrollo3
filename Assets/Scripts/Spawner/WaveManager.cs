using TMPro;
using UnityEngine;
using System.Collections;


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
    public GameObject waveName;
    public TMP_Text roundText;

    private Wave _currentWave;
    private int _currentWaveIndex;
    private float _nextSpawnTime;
    private bool _canSpawn = true;

    private void Update()
    {
        _currentWave = waves[_currentWaveIndex];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        roundText.text = "Wave: " + (_currentWaveIndex + 1f).ToString();

        if (totalEnemies.Length == 0)
        {
            if (!_canSpawn && _currentWaveIndex + 1 != waves.Length)
            {
                StartCoroutine(WaveShowUI(1));
                SpawnNextWave();
            }
            else
            {
                Debug.Log("Game Finished");
            }
        }
    }

    public IEnumerator WaveShowUI(int index)
    {
        waveName.SetActive(true);
        waveName.GetComponent<TextMeshProUGUI>().text = "Wave: " + waves[_currentWaveIndex + index].waveIndex;
        yield return new WaitForSeconds(3f);
        waveName.SetActive(false);
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