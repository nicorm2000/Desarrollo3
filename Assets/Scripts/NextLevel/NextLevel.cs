using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string levelName;
    public void LoadLevel() 
    {
        SceneManager.LoadScene(levelName);
    }
}