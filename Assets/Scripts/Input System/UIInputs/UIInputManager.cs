using UnityEngine;
using UnityEngine.EventSystems;

public class UIInputManger : MonoBehaviour
{
    [Header("Transition Dependencies")]
    [SerializeField] private Transitions increaseSizeOff;

    [Header("Pause Menu Dependencies")]
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject resumeButton;

    [Header("Mini Map Dependencies")]
    [SerializeField] private MiniMap miniMap;

    [Header("Cursor Dependencies")]
    [SerializeField] private CursorObject cursorObject;

    private UIInputs action;

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
        if (pauseMenu.isPaused)
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
        if (miniMap.isMapActive && !pauseMenu.isPaused)
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
        if (!miniMap.isMapActive)
        {
            miniMap.DeactivateMap();
        }
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