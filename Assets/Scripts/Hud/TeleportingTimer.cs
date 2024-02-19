using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeleportingTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerToTeleport;

    private string text = "Returning to the arena in\r\n\r\n";

    public float maxTime;
    public float currentTime;

    private void Start()
    {
        gameObject.SetActive(false);
        currentTime = maxTime;
    }

    private void Update()
    {
        timerToTeleport.text = (text + (int)currentTime).ToString();

        if (currentTime <= 0) 
        {
            currentTime = 0;
        }

        else 
        {
            currentTime -= Time.deltaTime;
        }
    }
}
