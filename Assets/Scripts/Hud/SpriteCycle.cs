using UnityEngine;
using UnityEngine.UI;

public class SpriteCycle : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private KeyCode keyCode = KeyCode.Alpha2;
    [SerializeField] private Sprite[] sprites;

    private int _currentIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            _currentIndex = (_currentIndex + 1) % sprites.Length;
            image.sprite = sprites[_currentIndex];
        }
    }
}