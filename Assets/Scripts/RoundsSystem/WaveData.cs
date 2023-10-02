using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Custom/WaveData")]

public class WaveData : ScriptableObject
{
    public int currentRound;
    public int maxRounds;
    public int currentEnemies;
    public int maxEnemies;
}
