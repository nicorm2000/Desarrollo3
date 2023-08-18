using System;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }

    [SerializeField] private List<CursorAnimation> cursorAnimationList;
    
    private CursorAnimation _cursorAnimation;
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
        Instance = this;
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

    private CursorAnimation GetCursorAnimation(CursorType cursorType)
    {
        foreach (CursorAnimation cursorAnimation in cursorAnimationList)
        {
            if (cursorAnimation.cursorType == cursorType)
                return cursorAnimation;
        }

        // Couldn't find this cursor type
        return null;
    }

    private void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this._cursorAnimation = cursorAnimation;

        _currentFrame = 0;

        _frameTimer = _cursorAnimation.frameRate;

        _frameCount = _cursorAnimation.textureArray.Length;
    }

    [Serializable]
    public class CursorAnimation
    { 
        public CursorType cursorType;
        public Texture2D[] textureArray;
        public float frameRate;
        public Vector2 offset;
    }
}