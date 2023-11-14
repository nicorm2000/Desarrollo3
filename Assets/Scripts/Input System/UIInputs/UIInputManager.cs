using UnityEngine;
using UnityEngine.EventSystems;

public class UIInputManger : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private Transitions increaseSizeOff;

    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private GameObject resumeButton;

    [SerializeField] private MiniMap miniMap;

    [SerializeField] private CursorObject cursorObject;

    UIInputs action;

    private void Awake()
    {
        action = new UIInputs();
    }

    void Start()
    {
        increaseSizeOff.ActiveTransition();

        pauseMenu = GetComponent<PauseMenu>();

        action.UI.Pause.performed += _ => IsPaused();

        action.UI.Map.performed += _ => IsMiniMap();
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

    private void IsMiniMap()
    {
        if (miniMap.isMapActive)
        {
            ActivateMiniMap();
        }
        else
        {
            DeativateMiniMap();
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
        cursorObject.MouseNotInteract();
        pauseMenu.Resume();
    }

    public void ActivateMiniMap()
    {
        miniMap.ActivateMap();
    }

    public void DeativateMiniMap()
    {
        miniMap.DeactivateMap();
    }
}