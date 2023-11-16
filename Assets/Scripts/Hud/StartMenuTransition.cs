using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuTransition : MonoBehaviour
{
    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOff;
    private float timeToWait = 1f;

    void Start()
    {
        StartCoroutine(increaseSizeOff.ActiveTransition(timeToWait));
        StartCoroutine(increaseSizeOff.DisableTransition(timeToWait));
    }
}
