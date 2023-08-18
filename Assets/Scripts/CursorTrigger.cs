using UnityEngine;

public class CursorTrigger : MonoBehaviour
{
    [SerializeField] private CursorManager cursorManager;
    [SerializeField] private LayerMask cursorLayer;
    [SerializeField] private int newCursorIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & cursorLayer) != 0)
        {
            cursorManager.SetNewCursor(newCursorIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & cursorLayer) != 0)
        {
            cursorManager.SetNewCursor(0); // Set back to the default cursor index
        }
    }
}