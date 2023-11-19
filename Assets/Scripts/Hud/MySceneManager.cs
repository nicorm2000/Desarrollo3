using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOn;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string click;

    private float timeToWait = 1f;
    private string sceneName = "Null";

    public void LoadSceneWithTransition() 
    {
        StartCoroutine(increaseSizeOn.ActiveTransition(timeToWait));
        StartCoroutine(increaseSizeOn.DisableTransition(timeToWait));

        audioManager.PlaySound(click);
        Invoke("LoadScene", 1f);
    }

    private void LoadScene()
    {
        LoadSceneByName(sceneName);
    }

    public void SetSceneName(string newName) 
    {
        sceneName = newName;
    }

    /// <summary>
    /// Loads a scene by its name.
    /// </summary>
    /// <param name="name">The name of the scene to load.</param>
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Exits the application.
    /// </summary>
    public void Exit()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
        audioManager.PlaySound(click);
        Application.Quit();
    }

    /// <summary>
    /// Opens a URL in the default web browser.
    /// </summary>
    /// <param name="link">The URL to open.</param>
    public void OpenURL(string link)
    {
        audioManager.PlaySound(click);
        Application.OpenURL(link);
    }
}