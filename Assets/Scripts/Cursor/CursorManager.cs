using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    [SerializeField] private List<CursorAnimationData> cursorAnimationDataList;
    
    private CursorAnimationData _cursorAnimation;
    private int _currentFrame;
    private int _frameCount;
    private float _frameTimer;

    public enum CursorType
    {
        RedDot,
        Cross
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);

            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetActiveCursorType(CursorType.RedDot);
    }

    private void Update()
    {
        _frameTimer -= Time.deltaTime;

        if (_frameTimer <= 0 )
        {
            _frameTimer += _cursorAnimation.frameRate;

            _currentFrame = (_currentFrame + 1) % _frameCount;

            Cursor.SetCursor(_cursorAnimation.textureArray[_currentFrame], _cursorAnimation.offset, CursorMode.Auto);
        }
    }

    public void SetActiveCursorType(CursorType cursorType)
    {
        SetActiveCursorAnimation(GetCursorAnimation(cursorType));
    }

    private CursorAnimationData GetCursorAnimation(CursorType cursorType)
    {
        foreach (CursorAnimationData cursorAnimationData in cursorAnimationDataList)
        {
            if (cursorAnimationData.cursorType == cursorType)
                return cursorAnimationData;
        }

        return null; // Return null if no matching cursor animation data is found
    }

    private void SetActiveCursorAnimation(CursorAnimationData cursorAnimation)
    {
        _cursorAnimation = cursorAnimation;

        _currentFrame = 0;

        _frameTimer = _cursorAnimation.frameRate;

        _frameCount = _cursorAnimation.textureArray.Length;
    }
}