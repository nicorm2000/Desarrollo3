using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWeapons : MonoBehaviour
{
    [Header("Unlock Weapons Dependences")]
    [SerializeField] private WaveManager waveManager;

    public int unlockEpicWeapons = 4;

    public int unlockSuperWeapons = 9;

    [Header("Weapons to Unlock")]

    [SerializeField] private GameObject[] weapons;
    
    [SerializeField] private GameObject[] blackWeapons;

    private void Start()
    {
        for (int i = 0; i < blackWeapons.Length; i++)
        {
            blackWeapons[i].SetActive(true);
        }

        for (int i = 0; i < weapons.Length; i++) 
        {
            weapons[i].SetActive(false);
        }
    }

    void Update()
    {
        if (waveManager.currentWaveIndex >= unlockEpicWeapons) 
        {
            blackWeapons[0].SetActive(false);
            blackWeapons[1].SetActive(false);

            weapons[0].SetActive(true);
            weapons[1].SetActive(true);
        }

        if (waveManager.currentWaveIndex >= unlockSuperWeapons)
        {
            blackWeapons[2].SetActive(false);
            blackWeapons[3].SetActive(false);

            weapons[2].SetActive(true);
            weapons[3].SetActive(true);
        }
    }
}
