using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text roundText;

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
        roundText.text = "Rounds: " + currentRound.ToString();
    }

    public void IncreaseRounds(int round) 
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
