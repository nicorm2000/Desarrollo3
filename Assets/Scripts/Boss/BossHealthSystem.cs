using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthSystem : MonoBehaviour
{
    [SerializeField] private BossData bossData;

    public void TakeDamage(float damage) 
    {
        bossData.health -= damage;
    }

    public void BossDies() 
    {
        bossData.isDead = true;
    }
}
