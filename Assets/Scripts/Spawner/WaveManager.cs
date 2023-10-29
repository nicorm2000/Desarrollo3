using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private IWave[] _wave;
    private int _currentWaveIndex = 0;

    private void Start()
    {
        _wave = new IWave[]
        {
            new Wave1(),
            new Wave2(),
            new Wave3()
        };

        ActivateCurrentWave();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DeactivateCurrentWave();
            SwitchToNextWave();
        }
    }

    private void ActivateCurrentWave()
    {
        _wave[_currentWaveIndex].Activate();
    }

    private void DeactivateCurrentWave()
    {
        _wave[_currentWaveIndex].Deactivate();
    }

    private void SwitchToNextWave()
    {
        if (_currentWaveIndex < _wave.Length - 1)
        {
            _currentWaveIndex++;
            ActivateCurrentWave();
        }
        else 
        {
            Debug.Log("Final wave");
            _currentWaveIndex = _wave.Length - 1;
        }
    }
}