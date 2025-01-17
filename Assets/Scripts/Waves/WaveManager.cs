using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [Header("Wave ID")]
    public short waveIndex;

    [Header("Wave Properties")]
    public float spawnInterval;

    [Header("Enemies")]
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
    [SerializeField] private string waveBeforeTako;

    private bool basketSFXHasPlayed = false;

    [Header("Enemies Dependencies")]
    [SerializeField] private EnemyData[] enemyData;

    public int currentWaveIndex;

    public bool iAmInShop = false;

    private int _maxWaves = Constants.ROUNDS_BETWEEN_SHOPS;
    private Wave _currentWave;
    private bool _canSpawn = true;
    private bool _nextWave = false;
    private bool _finishedWaves = false;
    private bool _popUpActive = false;

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

        if (_canSpawn)
        {
            SpawnWave();
        }

        if (playerData._isDead)
        {
            ResetWaves();
        }

        if (EnemyManager.enemyCount == Constants.ZERO && !playerData._isDead)
        {
            if (waves[currentWaveIndex].waveIndex == _maxWaves - Constants.ONE && !iAmInShop)
            {
                if (!_popUpActive)
                {
                    if (!AudioManager.muteSFX)
                    {
                        audioManager.PlaySound(waveBegins);
                    }
                    ActivateShop();
                }
                return;
            }

            _nextWave = true;
        }

        if (!_canSpawn && _nextWave)
        {
            WaveUpdater();
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
    /// Updates to the next wave.
    /// </summary>
    private void WaveUpdater()
    {
        if (currentWaveIndex + Constants.ONE != waves.Length && !playerData._isDead)
        {
            SpawnNextWave();
        }
        else
        {
            if (!_finishedWaves)
            {
                StartCoroutine(waveUI.ShowWaveCompletedUI());
                Debug.Log("Boss Fight Spawn");
                ActivateShop();
                _finishedWaves = true;

                if (!AudioManager.muteSFX)
                {
                    audioManager.PlaySound(waveBeforeTako);
                }
            }
        }
        basketSFXHasPlayed = false;
        _nextWave = false;
    }

    /// <summary>
    /// Spawns the next wave of enemies.
    /// </summary>
    private void SpawnNextWave()
    {
        abilities.DestroySlowers();
        currentWaveIndex++;
        if (waves[currentWaveIndex].waveIndex != _maxWaves - Constants.ONE - Constants.ROUNDS_BETWEEN_SHOPS && !_popUpActive)
        {
            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(waveBegins);
            }
            shop.ActivatePopUp();
        }
        StartCoroutine(waveUI.ShowWaveUI(waves[currentWaveIndex].waveIndex));
        _popUpActive = false;
        _canSpawn = true;
    }

    /// <summary>
    /// Calls the coroutine in charge of spawning enemies.
    /// </summary>
    private void SpawnWave()
    {
        StartCoroutine(SpawnWaveCoroutine());
    }

    /// <summary>
    /// Spawns individual enemies within the current wave.
    /// </summary>
    private IEnumerator SpawnWaveCoroutine()
    {
        _canSpawn = false;
        for (int i = 0; i < _currentWave.enemiesAmount.enemies.Length; i++)
        {
            GameObject enemyPrefab = _currentWave.enemiesAmount.enemies[i];
            int enemyAmount = _currentWave.enemiesAmount.enemyAmount[i];

            for (int j = 0; j < enemyAmount; j++)
            {
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                EnemyManager.enemyCount++;
                Instantiate(enemyPrefab, new Vector3(randomSpawnPoint.position.x, randomSpawnPoint.position.y, randomSpawnPoint.position.z - Constants.Z_VALUE_OFFSET), Quaternion.identity);
                yield return new WaitForSeconds(_currentWave.spawnInterval);
            }
        }
    }

    /// <summary>
    /// Activates the shop UI elements.
    /// </summary>
    private void ActivateShop()
    {
        shop.ActivatePopUp();
        basket.SetActive(true);
        door.SetActive(true);
        baoBasketIndicator.SetActive(true);

        _popUpActive = true;
        StartCoroutine(WaitForBasketToFall());
    }

    /// <summary>
    /// Sets the number of waves before the next shop becomes available.
    /// </summary>
    public void SetShopWaves()
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
        EnemyManager.enemyCount = 0;
    }

    private IEnumerator WaitForBasketToFall()
    {
        if (!basketSFXHasPlayed)
        {
            basketSFXHasPlayed = true;
            yield return new WaitForSeconds(playerData.shopFallTimer);
            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(playerData.shopFall);
            }
        }
    }
}