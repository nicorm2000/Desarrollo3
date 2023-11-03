using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string[] sceneName;

    public void LoadTutorial()
    {
            SceneManager.LoadScene(sceneName[1]);
    }

    public void LoadGameLevel()
    {
        SceneManager.LoadScene(sceneName[2]);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(sceneName[3]);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(sceneName[0]);
    }

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

    public void OpenURL(string link)
    {
        Application.OpenURL(link);
    }
}