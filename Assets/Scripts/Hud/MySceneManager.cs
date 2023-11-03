using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
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
        Application.Quit();
    }

    /// <summary>
    /// Opens a URL in the default web browser.
    /// </summary>
    /// <param name="link">The URL to open.</param>
    public void OpenURL(string link)
    {
        Application.OpenURL(link);
    }
}