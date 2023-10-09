using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] string[] sceneName;

    public void LoadLevel() 
    {
        SceneManager.LoadScene(sceneName[0]);
    }

    public void LoadMenu() 
    {
        SceneManager.LoadScene(sceneName[1]);
    }

    public void Exit() 
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
