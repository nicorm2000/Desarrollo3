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

    [Header("Camera Shake Configuration")]
    [SerializeField] private ScreenShake screenShake;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve animationCurve;

    [Header("Visualization")]
    [SerializeField] private Color detectionRadiusColor = Color.yellow;
    [SerializeField] private Color explosionRadiusColor = Color.red;
    [SerializeField] private GameObject smallRadius;
    [SerializeField] private GameObject mediumRadius;
    [SerializeField] private GameObject bigRadius;
    [SerializeField] private GameObject smokeRadius;
    [SerializeField] private float smokeDuration;

    private GameObject target;

    private bool canExplode = true;
    private bool countdownStarted = false;
    private float damage;
    private BoxCollider enemyCollider = null;

    private void Start()
    {
        damage = enemyData.damage;
        target = GameObject.FindWithTag("Player");
        enemyCollider = gameObject.GetComponent<BoxCollider>();
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

        smallRadius.SetActive(true);

        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (timer <= explosionCooldown * 0.66f)
            {
                mediumRadius.SetActive(true);
            }
            if (timer <= explosionCooldown * 0.33f)
            {
                bigRadius.SetActive(true);
            }

            yield return null;
        }

        bigRadius.SetActive(false);
        mediumRadius.SetActive(false);
        smallRadius.SetActive(false);

        Explode();

        //This can help the player not receive damage if we make this enemy do damage while being melee
        enemyCollider.enabled = false;

        smokeRadius.SetActive(true);
        yield return new WaitForSeconds(smokeDuration);
        smokeRadius.SetActive(false);

        Destroy(gameObject);
    }

    private void Explode()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerData.transform.position);

        if (distanceToPlayer <= explosionRadius)
        {
            StartCoroutine(screenShake.Shake(duration, animationCurve));
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