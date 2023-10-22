using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float enemyDamage;
    [SerializeField] private HealthSystem enemyHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<HealthSystem>().TakeDamage(enemyDamage);
        }
    }
}