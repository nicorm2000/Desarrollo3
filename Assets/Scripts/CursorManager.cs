using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private List<Texture2D> cursortexture;

    private Vector2 _cursorHotSpot;

    public void SetNewCursor(int index)
    {
        if (index >= 0 && index < cursortexture.Count)
        {
            _cursorHotSpot = new Vector2(cursortexture[index].width / 2, cursortexture[index].height / 2);

            Cursor.SetCursor(cursortexture[index], _cursorHotSpot, CursorMode.Auto);
        }
        else
        {
            Debug.LogError("Invalid cursor index!");
        }
    }
}