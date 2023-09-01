using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRounds : MonoBehaviour
{
    [SerializeField] private RoundCounter roundCounter;

    [SerializeField] private Shop shop;

    [SerializeField] private int addRound = 1;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            roundCounter.IncreaseRounds(addRound);
            shop.ActiveShop();
        }
    }
}
