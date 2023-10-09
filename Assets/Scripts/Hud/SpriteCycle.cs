using UnityEngine;
using UnityEngine.UI;

public class SpriteCycle : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;

    private int _currentIndex = 0;

    public void UpdateStatsUI(float stack) 
    {
        stack = _currentIndex;
        _currentIndex = (_currentIndex + 1) % sprites.Length;
        image.sprite = sprites[_currentIndex];
    }
}