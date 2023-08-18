using UnityEngine;

[CreateAssetMenu(fileName = "New Cursor Animation Data", menuName = "Cursor Animation Data")]
public class CursorAnimationData : ScriptableObject
{
    public CursorManager.CursorType cursorType;
    public Texture2D[] textureArray;
    public float frameRate;
    public Vector2 offset;
}