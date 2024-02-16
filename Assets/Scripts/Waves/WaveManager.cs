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

    [Header("Enemies Amount")]
    public EnemiesAmount enemiesAmount;
}

[System.Serializable]
public class EnemiesAmount
{
    public GameObject[] enemies;
    public int[] enemyAmount;
}

public class WaveManager : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject basket;

    [Header("Shop Dependencies")]
    [SerializeField] private Shop shop;

    [Header("Player Dependencies")]
    [SerializeField] private PlayerData playerData;

    [Header("Wave Configuration")]
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Wave UI Dependencies")]
    [SerializeField] private WaveUI waveUI;

    [Header("Abilities Dependencies")]
    [SerializeField] private Abilities abilities;

    [Header("Bao Basket Inidicator Dependencies")]
    [SerializeField] private GameObject baoBasketIndicator;

    [Header("Audio Manager")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] private string waveBegins;

    [Header("Enemies Dependencies")]
    [SerializeField] private EnemyData[] enemyData;

    public int currentWaveIndex;

    private int _maxWaves = Constants.ROUNDS_BETWEEN_SHOPS;
    private Wave _currentWave;
    private float _nextSpawnTime;
    private bool _canSpawn = true;
    private bool _nextWave = false;

    private void OnValidate()
    {
        if (waves == null)
        {
            return;
        }

        for (int i = 0; i < waves.Length; i++)
        {
            if (waves[i].enemiesAmount == null)
            {
                return;
            }

            if (waves[i].enemiesAmount.enemyAmount != null || waves[i].enemiesAmount.enemyAmount.Length == 0)
            {
                if (waves[i].enemiesAmount.enemies != null || waves[i].enemiesAmount.enemies.Length == 0)
                {
                    if (waves[i].enemiesAmount.enemyAmount.Length != waves[i].enemiesAmount.enemies.Length)
                    {
                        waves[i].enemiesAmount.enemyAmount = new int[waves[i].enemiesAmount.enemies.Length];
                    }
                }
            }
        }
    }

    /// <summary>
    /// Starts the game by showing the current wave index on the wave UI.
    /// </summary>
    private void Start()
    {
        ResetWaves();
        ResetEnemiesValues();
        waveUI.ShowWaveText(waves[currentWaveIndex].waveIndex);
    }

    /// <summary>
    /// Updates the game state, spawns waves, activates the shop, and handles wave progression.
    /// </summary>
    private void Update()
    {
        _currentWave = waves[currentWaveIndex];
        SpawnWave();

        if (playerData._isDead == true)
        {
            ResetWaves();
        }

        if (HealthSystem.enemyCount != Constants.ZERO)
        {
            if (waves[currentWaveIndex].waveIndex == _maxWaves)
            {
                ActivateShop();
                SetShopWaves();
            }

            _nextWave = false;
        }

        if (HealthSystem.enemyCount == Constants.ZERO)
        {
            _nextWave = true;
        }

        if (_canSpawn == false && _nextWave == true)
        {
            if (currentWaveIndex + Constants.ONE != waves.Length && playerData._isDead == false)
            {
                if (!AudioManager.muteSFX)
                {
                    audioManager.PlaySound(waveBegins);
                }
                
                SpawnNextWave();
            }
            else
            {
                Debug.Log("Game Finished");
                StartCoroutine(waveUI.ShowWaveCompletedUI());
            }

            _nextWave = false;
        }
    }

    /// <summary>
    /// Updates the wave value and displays the corresponding wave text on the UI.
    /// </summary>
    public void UpdateWaveValue()
    {
        if (currentWaveIndex >= waves.Length - Constants.ONE)
        {
            return;
        }
        else
        {
            currentWaveIndex++;
            Debug.Log(currentWaveIndex);
            Debug.Log(waves.Length - Constants.ONE);
            waveUI.ShowWaveText(waves[currentWaveIndex].waveIndex);
        }
    }

    /// <summary>
    /// Spawns the next wave of enemies.
    /// </summary>
    private void SpawnNextWave()
    {
        abilities.DestroySlowers();
        currentWaveIndex++;
        shop.ActivatePopUp();
        StartCoroutine(waveUI.ShowWaveUI(waves[currentWaveIndex].waveIndex));
        _canSpawn = true;
    }

    /// <summary>
    /// Spawns individual enemies within the current wave.
    /// </summary>
    private void SpawnWave()
    {
        if (_canSpawn && _nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = _currentWave.enemyType[Random.Range(Constants.ZERO, _currentWave.enemyType.Length)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(Constants.ZERO, spawnPoints.Length)];
            Instantiate(randomEnemy, new Vector3(randomSpawnPoint.position.x, randomSpawnPoint.position.y, randomSpawnPoint.position.z - Constants.Z_VALUE_OFFSET), Quaternion.identity);

            _currentWave.numberOfEnemies--;
            _nextSpawnTime = Time.time + _currentWave.spawnInterval;

            if (_currentWave.numberOfEnemies <= Constants.ONE)
            {
                _canSpawn = false;
            }
        }
    }

    /// <summary>
    /// Activates the shop UI elements.
    /// </summary>
    private void ActivateShop()
    {
        basket.SetActive(true);
        baoBasketIndicator.SetActive(true);
        door.SetActive(true);
    }

    /// <summary>
    /// Sets the number of waves before the next shop becomes available.
    /// </summary>
    private void SetShopWaves()
    {
        _maxWaves += Constants.ROUNDS_BETWEEN_SHOPS;
    }

    private void ResetEnemiesValues()
    {
        for (int i = 0; enemyData.Length > i; i++)
        {
            enemyData[i].ResetEnemiesValues();
        }
    }

    private void ResetWaves()
    {
        HealthSystem.enemyCount = 0;
    }
}