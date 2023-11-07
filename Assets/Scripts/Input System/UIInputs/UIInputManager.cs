using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInputManger : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private GameObject resumeButton;

    UIInputs action;

    private void Awake()
    {
        action = new UIInputs();
    }

    void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();

        action.UI.Pause.performed += _ => IsPaused();
    }

    private void IsPaused()
    {
        if (pauseMenu.isPaused == true)
        {
            ResumeGame();
            EventSystem.current.SetSelectedGameObject(null);
        }

        else
        {
            PauseGame();
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }
    }

    public void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    public void PauseGame()
    {
        pauseMenu.Pause();
    }

    public void ResumeGame()
    {
        pauseMenu.Resume();
    }
}