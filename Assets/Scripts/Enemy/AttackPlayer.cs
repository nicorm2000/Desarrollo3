using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [Header("Interacting Layers")]
    [SerializeField] private LayerMask includeLayer;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    [Header("Enemy Data Dependencies")]
    [SerializeField] private EnemyData enemyData;

    private float damage;

    private void Start()
    {
        damage = enemyData.damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((Constants.ONE << other.gameObject.layer) & includeLayer) != Constants.ZERO && !playerData.isDashing)
        {
            Debug.Log("a");
            other.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
        }
    }
}