using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Shop Dependencies")]
    [SerializeField] private Shop shop;

    [Header("Pause Configuration")]
    [SerializeField] private GameObject pauseMenuUI;
    public bool isPaused = false;

    [Header("Pause Visual Configuration")]
    [SerializeField] private GameObject pausePlaceHolder;
    [SerializeField] private GameObject optionsPlaceHolder;
    [SerializeField] private Image pauseImage;
    [SerializeField] private Image optionsImage;
    [SerializeField] private Sprite activeButton;
    [SerializeField] private Sprite inactiveButton;

    [Header("Scene Manager Dependencies")]
    [SerializeField] private MySceneManager mySceneManager;
    [SerializeField] private string sceneName;

    [Header("Audio Manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private string click;

    /// <summary>
    /// Resumes the game by setting the time scale to 1, disabling pause state, and hiding the pause menu UI.
    /// </summary>
    public void Resume()
    {
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(click);
        }
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuUI.SetActive(false);
    }

    /// <summary>
    /// Pauses the game by setting the time scale to 0, enabling pause state, and showing the pause menu UI.
    /// </summary>
    public void Pause()
    {
        if (!shop.isPopUpActive)
        {
            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(click);
            }
            Time.timeScale = 0f;
            isPaused = true;
            pauseMenuUI.SetActive(true);
        }
    }

    /// <summary>
    /// Goes back to the main menu by setting the time scale to 1 and loading the specified scene.
    /// </summary>
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Exits the game by quitting the application.
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
#endif
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(click);
        }
        Application.Quit();
    }

    /// <summary>
    /// Changes the image of a button based on the active state of a specified object.
    /// </summary>
    /// <param name="image">The Image component of the button.</param>
    /// <param name="objectToCheck">The GameObject to check the active state.</param>
    public void ChangeButtonImage(Image image, GameObject objectToCheck)
    {
        if (objectToCheck.activeSelf)
        {
            image.sprite = activeButton;
        }
        else 
        {
            image.sprite = inactiveButton;
        }
    }

    /// <summary>
    /// Activates the pause window by hiding the options placeholder, showing the pause placeholder, and updating button images.
    /// </summary>
    public void ActivatePauseWindow()
    {
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(click);
        }
        optionsPlaceHolder.SetActive(false);
        pausePlaceHolder.SetActive(true);
        ChangeButtonImage(pauseImage, pausePlaceHolder);
        ChangeButtonImage(optionsImage, optionsPlaceHolder);
    }

    /// <summary>
    /// Activates the options window by hiding the pause placeholder, showing the options placeholder, and updating button images.
    /// </summary>
    public void ActivateOptionsWindow()
    {
        if (!AudioManager.muteSFX)
        {
            audioManager.PlaySound(click);
        }
        pausePlaceHolder.SetActive(false);
        optionsPlaceHolder.SetActive(true);
        ChangeButtonImage(optionsImage, optionsPlaceHolder);
        ChangeButtonImage(pauseImage, pausePlaceHolder);
    }
}