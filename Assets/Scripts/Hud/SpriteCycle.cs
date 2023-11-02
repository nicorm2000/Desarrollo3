using UnityEngine;
using UnityEngine.UI;

public class SpriteCycle : MonoBehaviour
{
    [Header("Image to Modify")]
    [SerializeField] private Image image;

    [Header("Sprites")]
    [SerializeField] private Sprite[] sprites;

    private int _currentIndex = 0;

    /// <summary>
    /// Updates the UI to display the next sprite in the sprites array based on the current stack value.
    /// </summary>
    /// <param name="stack">The current stack value.</param>
    public void UpdateStatsUI(float stack)
    {
        _currentIndex = Mathf.FloorToInt(stack) % sprites.Length;
        image.sprite = sprites[_currentIndex];
    }
}