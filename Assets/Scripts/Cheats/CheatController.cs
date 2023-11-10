using UnityEngine;

public class CheatController : MonoBehaviour
{
    [Header("Cheats")]
    [SerializeField] private GameObject cheatsText;
    
    [Header("Wave Manager")]
    [SerializeField] private GameObject enemyWaveUpdater;
    [SerializeField] private WaveManager waveManager;

    /// <summary>
    /// Starts the cheats.
    /// </summary>
    private void Start()
    {
        cheatsText.SetActive(true);
        enemyWaveUpdater.SetActive(true);
    }

    /// <summary>
    /// Updates the wave in the wave manager.
    /// </summary>
    public void UpdateWave()
    {
        waveManager.UpdateWaveValue();
    }
}