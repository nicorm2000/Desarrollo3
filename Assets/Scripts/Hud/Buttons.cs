using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] string[] sceneName;

    public void Restart() 
    {
        SceneManager.LoadScene(sceneName[0]);
    }

    public void ReturnMenu() 
    {
        SceneManager.LoadScene(sceneName[1]);
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
