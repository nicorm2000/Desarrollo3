using System;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [Header("Setup")]
    public float chaseSpeed;

    public event Action<bool> onEnemyWalkChange;

    private bool isWalking;

    [Header("References")]
    [SerializeField] private HealthSystem healthSystem;
    public EnemyData enemyData;
    public GameObject target;
    public FlipEnemy flipEnemy;

    [Header("Timer")]
    [SerializeField] private float maxTime = 1f;
    private float timer = 0f;

    private void Start()
    {
        chaseSpeed = enemyData.movementSpeed;
        target = GameObject.FindWithTag("Player");
        timer = maxTime;
    }

    private void Update()
    {
        EnemyMovement();

        if (healthSystem._dead)
        {
            onEnemyWalkChange?.Invoke(false);
        }
    }

    private void EnemyMovement()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = maxTime;
            isWalking = true;
        }

        Vector2 playerPosition = target.transform.position;
        Vector2 currentPosition = transform.position;

        enemyData.movementDirection = (playerPosition - currentPosition).normalized;

        RaycastHit2D hit = Physics2D.Raycast(currentPosition, enemyData.movementDirection, enemyData.avoidanceDistance);

        Debug.DrawRay(currentPosition, enemyData.movementDirection, Color.red);

        onEnemyWalkChange?.Invoke(isWalking);

        if (hit.collider != null)
        {
            Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;

            transform.Translate(avoidanceDirection * chaseSpeed * Time.deltaTime);

            CheckDirectionToFlip();
        }
        else
        {
            transform.Translate(enemyData.movementDirection * chaseSpeed * Time.deltaTime);

            CheckDirectionToFlip();
        }
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
}