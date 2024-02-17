using UnityEngine;

public class BossHealthSystem : MonoBehaviour
{
    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    public void SetHealth(float maxHealth) 
    {
        bossData.health = maxHealth;
    }

    public void TakeDamage(float damage) 
    {
        bossData.health -= damage;
    }

    public void BossDies() 
    {
        bossData.isDead = true;
    }
}