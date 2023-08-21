using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text roundText;
    
    [SerializeField] private int addRound = 1;

    public int currentRound = 1;

    public int maxRounds = 5;

    void Start()
    {
        roundText.text = "Rounds: " + currentRound.ToString();
    }

    void Update()
    {
        roundText.text = "Rounds: " + currentRound.ToString();

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            IncreaseRounds(addRound);
        }
    }

    private void IncreaseRounds(int round) 
    {
        currentRound += round;   
    }
}
