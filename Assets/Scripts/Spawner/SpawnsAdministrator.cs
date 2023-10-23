using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsAdministrator : MonoBehaviour
{
    [SerializeField] private Spawner[] spawns;

    public WaveData waveData;

    public void EnemiesBySpawnCalculator(int maxEnemies, int spawnsCounter)
    {
        waveData.currentEnemies = maxEnemies;
        spawns[0].amountToSpawn = maxEnemies / spawnsCounter;
        spawns[0].enemiesToSpawn = spawns[0].amountToSpawn;

        spawns[1].amountToSpawn = maxEnemies / spawnsCounter;
        spawns[1].enemiesToSpawn = spawns[1].amountToSpawn;

        spawns[2].amountToSpawn = maxEnemies / spawnsCounter;
        spawns[2].enemiesToSpawn = spawns[2].amountToSpawn;

        spawns[3].amountToSpawn = maxEnemies / spawnsCounter;
        spawns[3].enemiesToSpawn = spawns[3].amountToSpawn;
    }
}
