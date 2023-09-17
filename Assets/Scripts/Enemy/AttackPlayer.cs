using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] private float damage;

    PlayerHealth playerHealth;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.CompareTag("Player"))
        {
            playerHealth.takeDamage(damage);
        }
    }
}
