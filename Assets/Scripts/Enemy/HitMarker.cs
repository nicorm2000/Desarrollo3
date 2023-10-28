using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarker : MonoBehaviour
{
    [SerializeField] private SpriteRenderer enemy;

    private float timer;
    public float maxTimer;


    private void Start()
    {
        timer = maxTimer;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            timer = maxTimer;
            ResetEnemyColor();
        }   
    }

    public void HitEnemy() 
    {
        enemy.color = Color.red;
    }

    public void ResetEnemyColor() 
    {
        enemy.color = Color.white;
    }
}
