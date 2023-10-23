using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRounds : MonoBehaviour
{
    [SerializeField] private RoundCounter roundCounter;

    [SerializeField] private Shop shop;

    public WaveData waveData;

    void Update()
    {
        if (waveData.currentEnemies <= 0)
        {
            roundCounter.IncreaseRounds(waveData.addRound);
            shop.ActiveShop();
        }
    }
}
