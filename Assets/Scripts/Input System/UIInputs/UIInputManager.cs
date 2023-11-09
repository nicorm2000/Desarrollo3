using UnityEngine;
using UnityEngine.EventSystems;

public class UIInputManger : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private PauseMenu pauseMenu;

    [SerializeField] private GameObject resumeButton;

    [SerializeField] private MiniMap miniMap;
    [SerializeField] private GameObject miniM;

    UIInputs action;

    private void Awake()
    {
        action = new UIInputs();
    }

    void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();
        miniMap = GetComponent<MiniMap>();

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
        if (miniMap.isMapActive == true)
        {
            Debug.Log("a");
            ActivateMiniMap();
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            Debug.Log("b");
            DeativateMiniMap();
            EventSystem.current.SetSelectedGameObject(miniM);
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

    public void ActivateMiniMap()
    {
        miniMap.ActivateMap();
    }

    public void DeativateMiniMap()
    {
        miniMap.DeactivateMap();
    }
}