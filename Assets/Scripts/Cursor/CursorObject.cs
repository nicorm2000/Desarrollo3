using UnityEngine;

public class CursorObject : MonoBehaviour
{
    [SerializeField] private CursorManager cursor;

    public void MouseInteract()
    {
        cursor.SetActiveCursorType(CursorManager.CursorType.Interact);
    }

    public void MouseNotInteract()
    {
        cursor.SetActiveCursorType(CursorManager.CursorType.RotatingSquare);
    }
}