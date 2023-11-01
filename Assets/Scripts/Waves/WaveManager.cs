using UnityEngine;

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

    public int currentWaveIndex { get; private set; }

    private Wave _currentWave;
    private float _nextSpawnTime;
    private bool _canSpawn = true;

    [Header("UI")]
    public WaveUI waveUI;

    [Header("Abilities")]
    [SerializeField] private Abilities abilities;

    private void Start()
    {
        waveUI.ShowWaveText(waves[currentWaveIndex].waveIndex);
    }

    private void Update()
    {
        _currentWave = waves[currentWaveIndex];
        SpawnWave();

        if (HealthSystem.enemyCount != 0)
        {
            return;
        }

        if (waves[currentWaveIndex].waveIndex == _maxWaves)
        {
            ActivateShop();
            SetShopWaves();
        }

        if (!_canSpawn)
        {
            SpawnNextWave();
            if (currentWaveIndex + 1 != waves.Length)
            {
                popUp.ActivatePopUp();
                StartCoroutine(waveUI.WaveShowUI(waves[currentWaveIndex].waveIndex));
            }
            else
            {
                Debug.Log("Game Finished");
                StartCoroutine(waveUI.WaveCompletedShowUI());
            }
        }
    }

    private void SpawnNextWave()
    {
        abilities.DestroySlowers();
        currentWaveIndex++;
        _canSpawn = true;
    }

    private void SpawnWave()
    {
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
    }

    private void ActivateShop()
    {
        basket.SetActive(true);
        door.SetActive(true);
    }

    private void SetShopWaves()
    {
        _maxWaves += 5;
    }
}