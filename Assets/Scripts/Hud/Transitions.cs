using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject transition;
    private bool isEnded = false;

    public IEnumerator ActiveTransition(float timeToWait) 
    {
        isEnded = false;
        transition.SetActive(true);
        yield return new WaitForSeconds(timeToWait);
        isEnded = true;
    }

    public IEnumerator DisableTransition(float timeToWait)
    {
        isEnded = false;
        yield return new WaitForSeconds(timeToWait);
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
