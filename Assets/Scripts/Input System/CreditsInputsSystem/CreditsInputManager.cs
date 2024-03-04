using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsInputManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ReturnToMenu returnToMenu;

    public void OnOnInteract() 
    {
        returnToMenu.ReturnMenu();
    }
}
