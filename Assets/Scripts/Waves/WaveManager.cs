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
    [Header("GameObjects")]
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject basket;
    
    [Header("Shop Dependencies")]
    [SerializeField] private Shop shop;

    [Header("Wave Configuration")]
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Wave UI Dependencies")]
    [SerializeField] private WaveUI waveUI;

    [Header("Abilities Dependencies")]
    [SerializeField] private Abilities abilities;

    public int currentWaveIndex { get; private set; }

    private int _maxWaves = Constants.ROUNDS_BETWEEN_SHOPS;
    private Wave _currentWave;
    private float _nextSpawnTime;
    private bool _canSpawn = true;


    private void Start()
    {
        waveUI.ShowWaveText(waves[currentWaveIndex].waveIndex);
    }

    private void Update()
    {
        _currentWave = waves[currentWaveIndex];
        SpawnWave();

        if (HealthSystem.enemyCount != Constants.ZERO)
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
            if (currentWaveIndex + Constants.ONE != waves.Length)
            {
                shop.ActivatePopUp();
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
            GameObject randomEnemy = _currentWave.enemyType[Random.Range(Constants.ZERO, _currentWave.enemyType.Length)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(Constants.ZERO, spawnPoints.Length)];
            Instantiate(randomEnemy, new Vector3(randomSpawnPoint.position.x, randomSpawnPoint.position.y, randomSpawnPoint.position.z - Constants.Z_VALUE_OFFSET), Quaternion.identity);

            _currentWave.numberOfEnemies--;
            _nextSpawnTime = Time.time + _currentWave.spawnInterval;

            if (_currentWave.numberOfEnemies == Constants.ONE)
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
        _maxWaves += Constants.ROUNDS_BETWEEN_SHOPS;
    }
}