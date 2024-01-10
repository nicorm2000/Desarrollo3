using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExploder : MonoBehaviour
{
    [Header("Explosion Configuration")]
    [SerializeField] private float explosionCooldown;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionDamage;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    [Header("Enemy Data Dependencies")]
    [SerializeField] private EnemyData enemyData;

    private bool canExplode = true;
    private float damage;


    private void Start()
    {
        damage = enemyData.damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && canExplode)
        {
            Debug.Log("Boom");
            //Explode(other);
            //StartCoroutine(StartCooldown());
        }
    }

    //private void Explode(Collider player)
    //{
    //    player.GetComponent<PlayerHealth>().takeDamage(damage);
    //
    //    Destroy(gameObject);
    //}
    //
    //private IEnumerator StartCooldown()
    //{
    //    canExplode = false;
    //    yield return new WaitForSeconds(explosionCooldown);
    //    canExplode = true;
    //}
}