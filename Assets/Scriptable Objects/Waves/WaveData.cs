using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Custom/WaveData")]

public class WaveData : ScriptableObject
{
    [Header("RoundCounter")]
    public int currentRound;
    public int maxRounds;

    [Header("Spawner")]
    public int currentEnemies;
    public int maxEnemies;
    public int spawnsCounter;

    [Header("AddRound")]
    public int addRound;

    public void ResetWavesStacks() 
    {
        spawnsCounter = 3;
        maxEnemies = 3;
    }
}
