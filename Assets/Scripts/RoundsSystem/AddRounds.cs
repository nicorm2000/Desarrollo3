using UnityEngine;

public class AddRounds : MonoBehaviour
{
    [SerializeField] private RoundCounter roundCounter;
    [SerializeField] private Shop shop;

    public WaveData waveData;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            roundCounter.IncreaseRounds(waveData.addRound);
            shop.ActiveShop();
        }
    }
}