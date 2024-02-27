using UnityEngine;

public class BossHealthSystem : MonoBehaviour
{
    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    [Header("Hit Marker")]
    [SerializeField] private HitMarker hitMarker;

    [Header("Boss Health bar")]
    [SerializeField] private BossHealthBar bossHealthBar;
    [SerializeField] private GameObject goHealthBar;

    private void Start()
    {
        bossData.ResetBossData();
        bossData.currentHealth = bossData.maxHealth;
        bossHealthBar.SetMaxAndCurrentHealth(bossData.maxHealth, bossData.currentHealth);
    }

    private void Update()
    {
        bossHealthBar.SetHealth(bossData.currentHealth);
    }

    public void TakeDamage(float damage) 
    {
        if (!bossData.isDead) 
        {
            bossData.currentHealth -= damage;
            hitMarker.HitEnemy();
        }

        if(bossData.currentHealth <= 0) 
        {
            bossData.currentHealth = 0f;
            BossDies();
        }
    }

    public void BossDies() 
    {
        bossData.isDead = true;
        goHealthBar.SetActive(false);
    }
}