using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject transition;

    [Header("Setup")]
    [SerializeField] private float maxTime;
    private float timer;

    private bool startTrasition = false;
    private bool isEnded = false;

    private void Start()
    {
        timer = maxTime;
    }

    void Update()
    {
        if (startTrasition) 
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0) 
        {
            startTrasition = false;
            DisableTransition();
            timer = maxTime;
        }
    }

    public void ActiveTransition() 
    {
        isEnded = false;
        startTrasition = true;
        transition.SetActive(true);
    }
    
    public void DisableTransition() 
    {
        transition.SetActive(false);
        isEnded = true;
    }

    public bool isTransitionFinish() 
    {
        if (isEnded == true)
        return true;

        else 
        return false;
    }
}
