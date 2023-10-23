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

    [Header("AddRound")]
    public int addRound;
}
