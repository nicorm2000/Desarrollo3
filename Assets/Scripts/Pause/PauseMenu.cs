using UnityEditor;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private MySceneManager mySceneManager;
    [SerializeField] private string sceneName;
    [SerializeField] private Shop shop;

    public bool isPaused = false;

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuUI.SetActive(false);   
    }

    public void Pause()
    {
        if (!shop.isPopUpActive)
        {
            Time.timeScale = 0f;
            isPaused = true;
            pauseMenuUI.SetActive(true);

        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        mySceneManager.LoadSceneByName(sceneName);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
#endif
        Application.Quit();
    }
}