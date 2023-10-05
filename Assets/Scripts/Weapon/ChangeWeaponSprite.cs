using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] newSprite;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(int spriteNumber) 
    {
        spriteRenderer.sprite = newSprite[spriteNumber];   
    }
}
