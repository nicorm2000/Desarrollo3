using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIInputManger : MonoBehaviour
{
    [Header("Pause Menu Dependencies")]
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject resumeButton;

    [Header("Mini Map Dependencies")]
    [SerializeField] private MiniMap miniMap;

    [Header("Cursor Dependencies")]
    [SerializeField] private CursorObject cursorObject;

    private UIInputs action;

    public Vector2 mousePosition { get; private set; }

    private void Awake()
    {
        action = new UIInputs();
    }

    void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();

        action.UI.Pause.performed += _ => IsPaused();

        action.UI.Map.performed += _ => IsMiniMap();
    }

    public void OnMousePos(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
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
        if (!pauseMenu.isPaused)
        {
            if (!miniMap.isMapActive)
            {
                ActivateMiniMap();
            }
            else
            {
                DeativateMiniMap();
            }
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