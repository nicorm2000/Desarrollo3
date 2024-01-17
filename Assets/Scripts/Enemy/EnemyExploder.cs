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

    [Header("Screen Shake Configuration")]
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve animationCurve;

    [Header("Custom Screen Shake Configuration")]
    [SerializeField] private float maxShakeDistance;
    [SerializeField] private float minShakeDuration;
    [SerializeField] private float maxShakeDuration;
    [SerializeField] private AnimationCurve maxShakeAnimationCurve;

    [Header("Visualization")]
    [SerializeField] private Color detectionRadiusColor = Color.yellow;
    [SerializeField] private Color explosionRadiusColor = Color.red;
    [SerializeField] private GameObject smallRadius;
    [SerializeField] private GameObject mediumRadius;
    [SerializeField] private GameObject bigRadius;
    [SerializeField] private GameObject smokeRadius;
    [SerializeField] private float smokeDuration;

    private AIChase aiChase;

    private GameObject target;
    private GameObject screenShakeDependency;

    private bool hasExploded = false;
    private float damage;
    private BoxCollider enemyCollider = null;

    private void Start()
    {
        damage = enemyData.damage;
        target = GameObject.FindWithTag("Player");
        screenShakeDependency = GameObject.FindWithTag("MainCamera");
        enemyCollider = gameObject.GetComponent<BoxCollider>();
        aiChase = GetComponent<AIChase>();
        Debug.Log("D" + damage);
        Debug.Log("ED" + explosionDamage);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerData.transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            StartCoroutine(CountdownCoroutine());
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        float timer = explosionCooldown;

        aiChase.enabled = false;

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

        if (!hasExploded)
        {
            Explode();
            hasExploded = true;
        }

        enemyCollider.enabled = false;

        smokeRadius.SetActive(true);
        yield return new WaitForSeconds(smokeDuration);
        smokeRadius.SetActive(false);

        Destroy(gameObject);
    }

    private void Explode()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerData.transform.position);

        float distanceNormalized = Mathf.Clamp01(distanceToPlayer / maxShakeDistance);

        float adjustedShakeDuration = Mathf.Lerp(minShakeDuration, maxShakeDuration, distanceNormalized);
        AnimationCurve adjustedShakeAnimationCurve = new AnimationCurve(
            new Keyframe(0, 0),
            new Keyframe(1, maxShakeAnimationCurve.Evaluate(distanceNormalized))
        );

        StartCoroutine(screenShakeDependency.GetComponent<ScreenShake>().Shake(adjustedShakeDuration, adjustedShakeAnimationCurve));

        if (distanceToPlayer <= explosionRadius)
        {
            target.GetComponent<PlayerHealth>().takeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        DrawRadius(detectionRadius, detectionRadiusColor);

        DrawRadius(explosionRadius, explosionRadiusColor);

        DrawRadius(maxShakeDistance, explosionRadiusColor);
    }

    private void DrawRadius(float radius, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}