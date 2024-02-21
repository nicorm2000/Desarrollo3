using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExploder : MonoBehaviour
{
    public event Action<bool> onEnemyAttackChange;

    [Header("Detection Configuration")]
    [SerializeField] private float detectionRadius;

    [Header("Explosion Configuration")]
    [SerializeField] private float explosionBuildUp;
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

    private AIChase _aiChase;

    private GameObject _target;
    private GameObject _screenShakeDependency;

    private bool _hasExploded = false;
    private float _damage;
    private Collider _enemyCollider = null;

    private bool isAttacking;

    private void Start()
    {
        _damage = enemyData.damage;
        _target = EnemyManager.player;
        _screenShakeDependency = EnemyManager.mainCamera;
        _enemyCollider = gameObject.GetComponent<Collider>();
        _aiChase = GetComponent<AIChase>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerData.transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            isAttacking = true;
            onEnemyAttackChange?.Invoke(isAttacking);
            StartCoroutine(CountdownCoroutine());
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        float timer = explosionBuildUp;

        _aiChase.enabled = false;

        smallRadius.SetActive(true);

        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (timer <= explosionBuildUp * 0.66f)
            {
                mediumRadius.SetActive(true);
            }
            if (timer <= explosionBuildUp * 0.33f)
            {
                bigRadius.SetActive(true);
            }

            yield return null;
        }

        bigRadius.SetActive(false);
        mediumRadius.SetActive(false);
        smallRadius.SetActive(false);

        if (!_hasExploded)
        {
            Explode();
            _hasExploded = true;
        }

        _enemyCollider.enabled = false;

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

        StartCoroutine(_screenShakeDependency.GetComponent<ScreenShake>().Shake(adjustedShakeDuration, adjustedShakeAnimationCurve));

        if (distanceToPlayer <= explosionRadius)
        {
            _target.GetComponent<PlayerHealth>().takeDamage(_damage);
        }
    }

#if UNITY_EDITOR
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
#endif
}