using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExploder : MonoBehaviour
{
    [Header("Detection Configuration")]
    [SerializeField] private float detectionRadius;

    [Header("Explosion Configuration")]
    [SerializeField] private float explosionCooldown;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionDamage;

    [Header("Player Data Dependencies")]
    [SerializeField] private PlayerData playerData;

    [Header("Enemy Data Dependencies")]
    [SerializeField] private EnemyData enemyData;

    [Header("Visualization")]
    [SerializeField] private Color detectionRadiusColor = Color.yellow;
    [SerializeField] private Color explosionRadiusColor = Color.red;

    public GameObject target;

    private bool canExplode = true;
    private bool countdownStarted = false;
    private float damage;
    private float countdownTimer;

    private void Start()
    {
        damage = enemyData.damage;
        target = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerData.transform.position);

        if (distanceToPlayer <= detectionRadius && canExplode && !countdownStarted)
        {
            StartCoroutine(CountdownCoroutine());
            countdownStarted = true;
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        canExplode = false;

        float timer = explosionCooldown;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        Explode();
    }

    private void Explode()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerData.transform.position);

        if (distanceToPlayer <= explosionRadius)
        {
            target.GetComponent<PlayerHealth>().takeDamage(damage);
        }

        Debug.Log("Boom");

        canExplode = true;
        countdownStarted = false;
    }

    private void OnDrawGizmos()
    {
        DrawRadius(detectionRadius, detectionRadiusColor);

        DrawRadius(explosionRadius, explosionRadiusColor);
    }

    private void DrawRadius(float radius, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}