using System;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [Header("AI Chase Setup")]
    public float chaseSpeed;
    public event Action<bool> onEnemyWalkChange;

    [Header("Health System Dependencies")]
    [SerializeField] private HealthSystem healthSystem;

    [Header("Enemy Data Dependencies")]
    [SerializeField] private EnemyData enemyData;

    [Header("Flip Enemy Dependencies")]
    [SerializeField] private FlipEnemy flipEnemy;

    [Header("Timer")]
    [SerializeField] private float maxTime = 1f;

    private GameObject target;
    private bool isWalking;
    private float _timer = 0f;

    private void Start()
    {
        chaseSpeed = enemyData.movementSpeed;
        target = EnemyManager.player;
        _timer = maxTime;
        Debug.Log(target);
    }

    private void Update()
    {
        if (enemyData.canChase)
        {
            EnemyMovement();
        }

        if (healthSystem.dead)
        {
            onEnemyWalkChange?.Invoke(false);
        }
    }

    private void EnemyMovement()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer = maxTime;
            isWalking = true;
        }

        Vector2 playerPosition = target != null ? target.transform.position : Vector2.zero;
        Vector2 currentPosition = transform.position;

        float distanceToPlayer = Vector2.Distance(playerPosition, currentPosition);

        onEnemyWalkChange?.Invoke(isWalking);

        if (!enemyData.isGas)
        {
            enemyData.movementDirection = (playerPosition - currentPosition).normalized;

            transform.Translate(enemyData.movementDirection * chaseSpeed * Time.deltaTime);
        }
        else
        {
            if (distanceToPlayer <= enemyData.avoidanceDistance)
            {
                Vector2 toPlayer = playerPosition - currentPosition;
                Vector2 perpendicularDirection = new Vector2(-toPlayer.y, toPlayer.x).normalized;
                enemyData.movementDirection = Quaternion.Euler(0, 0, enemyData.circularMovementSpeed * Time.deltaTime) * perpendicularDirection;
            }
            else
            {
                enemyData.movementDirection = (playerPosition - currentPosition).normalized;
            }

            transform.Translate(enemyData.movementDirection * chaseSpeed * Time.deltaTime);
        }

        CheckDirectionToFlip();
    }

    private void CheckDirectionToFlip()
    {
        if (enemyData.movementDirection.x != 0)
        {
            flipEnemy.FlipEnemyX();
        }

        if (enemyData.movementDirection.y != 0)
        {
            flipEnemy.FlipEnemyY();
        }
    }

    public void SetEnemyChase(bool canChase)
    {
        enemyData.canChase = canChase;
    }

    public void EnemiesCanMove()
    {
        SetEnemyChase(true);
    }

    public void EnemiesCanNotMove()
    {
        SetEnemyChase(false);
    }
}