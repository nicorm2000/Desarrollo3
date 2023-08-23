using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text roundText;
    
    [SerializeField] private int addRound = 1;

    private static RoundCounter instance;

    public int currentRound = 1;

    public int maxRounds = 5;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        roundText.text = "Rounds: " + currentRound.ToString();
    }

    void Update()
    {
        PlayerPrefs.SetInt("CurrentRounds", currentRound);
        PlayerPrefs.Save();

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

    public static RoundCounter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RoundCounter>();
                
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    
                    instance = singletonObject.AddComponent<RoundCounter>();
                    
                    singletonObject.name = "RoundCounter (Singleton)";
                }
            }
            return instance;
        }
    }
}
