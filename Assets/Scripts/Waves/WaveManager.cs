using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Wave
{
    [Header("Wave ID")]
    public short waveIndex;

    [Header("Wave Properties")]
    public short numberOfEnemies;
    public GameObject[] enemyType;
    public float spawnInterval;
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject basket;
    [SerializeField] private Shop popUp;

    private int _maxWaves = 5;

    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject waveName;
    public GameObject waveCompleted;
    public TMP_Text roundText;
    public int currentWaveIndex;

    private Wave _currentWave;
    private float _nextSpawnTime;
    private bool _canSpawn = true;

    [Header("Abilities")]
    [SerializeField] private Abilities abilities;

    private void Update()
    {
        _currentWave = waves[currentWaveIndex];
        StartCoroutine(SpawnWave());

        if (HealthSystem.enemyCount == 0)
        {
            if (currentWaveIndex + 1 != waves.Length)
            {
                if (!_canSpawn)
                {
                    popUp.ActivatePopUp();
                    SpawnNextWave();
                    StartCoroutine(WaveShowUI());
                }
            }
            else
            {
                Debug.Log("Game Finished");
                waveCompleted.SetActive(true);
                SceneManager.LoadScene(5);
            }

            ActiveShop();
        }
    }

    public IEnumerator WaveShowUI()
    {
        waveName.SetActive(true);
        waveName.GetComponent<TextMeshProUGUI>().text = "Wave: " + waves[currentWaveIndex].waveIndex;
        yield return new WaitForSeconds(3f);
        waveName.SetActive(false);
    }

    private void SpawnNextWave()
    {
        abilities.DestroySlowers();
        currentWaveIndex++;
        _canSpawn = true;
    }

    private IEnumerator SpawnWave()
    {
        roundText.text = "Wave: " + (waves[currentWaveIndex].waveIndex).ToString();
        if (_canSpawn && _nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = _currentWave.enemyType[Random.Range(0, _currentWave.enemyType.Length)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, new Vector3(randomSpawnPoint.position.x, randomSpawnPoint.position.y, randomSpawnPoint.position.z + 0.5f), Quaternion.identity);

            _currentWave.numberOfEnemies--;
            _nextSpawnTime = Time.time + _currentWave.spawnInterval;

            if (_currentWave.numberOfEnemies == 0)
            {
                _canSpawn = false;
            }
        }

        yield return null;
    }

    private void ActiveShop() 
    {
        if (waves[currentWaveIndex].waveIndex == _maxWaves) 
        {
            basket.SetActive(true);
            door.SetActive(true);
            _maxWaves += 5;
        }
    }
}