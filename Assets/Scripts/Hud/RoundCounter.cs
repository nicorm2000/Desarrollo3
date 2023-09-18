using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text roundText;

    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private GameObject doorCollider;

    [SerializeField] private Spawner[] spawner;

    public int currentRound = 1;

    public int maxRounds = 5;

    void Start()
    {
        roundText.text = "Rounds: " + currentRound.ToString();
    }

    void Update()
    {
        roundText.text = "Rounds: " + currentRound.ToString();

        if (currentRound == maxRounds)
        {
            doorCollider.SetActive(true);
        }
    }

    public void IncreaseRounds(int round) 
    {
        currentRound += round;
        spawner[0].StartCoroutine(spawner[0].SpawnObjects());
        spawner[1].StartCoroutine(spawner[1].SpawnObjects());
        spawner[2].StartCoroutine(spawner[2].SpawnObjects());
    }
}
