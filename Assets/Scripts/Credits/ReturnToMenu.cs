using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string click;

    public string sceneName = "MainMenu";

    public void ReturnMenu()
    {
        Debug.Log("ReturnMenu");
        audioManager.StopSounds();

        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(click);
        }

        SceneManager.LoadScene(sceneName);
    }
}
