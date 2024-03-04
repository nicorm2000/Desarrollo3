using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string click;

    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOn;

    private float timeToWait = 1f;

    public string sceneName = "MainMenu";

    public string functionName = "LoadScene";

    public void ReturnMenu()
    {
        StartCoroutine(increaseSizeOn.ActiveTransition(timeToWait));

        audioManager.StopSounds();

        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(click);
        }

        Invoke(functionName, 1f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
