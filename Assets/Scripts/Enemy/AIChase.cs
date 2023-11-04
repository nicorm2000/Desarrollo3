using System;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [Header("Setup")]
    public float chaseSpeed;

    public event Action<bool> onEnemyWalkChange;

    private bool isWalking;

    [Header("References")]
    public EnemyData enemyData;
    public GameObject target;

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
        timer -= Time.deltaTime;

        if (timer <= 0f) 
        {
            timer = maxTime;
            isWalking = true;
        }

        Vector2 playerPosition = target.transform.position;
        Vector2 currentPosition = transform.position;

        Vector2 dirToPlayer = (playerPosition - currentPosition).normalized;

        RaycastHit2D hit = Physics2D.Raycast(currentPosition, dirToPlayer, enemyData.avoidanceDistance);

        Debug.DrawRay(currentPosition, dirToPlayer, Color.red);

        onEnemyWalkChange?.Invoke(isWalking);

        if (hit.collider != null)
        {
            Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
            transform.Translate(avoidanceDirection * chaseSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(dirToPlayer * chaseSpeed * Time.deltaTime);
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

        Vector2 dirToPlayer = (playerPosition - currentPosition).normalized;

        RaycastHit2D hit = Physics2D.Raycast(currentPosition, dirToPlayer, enemyData.avoidanceDistance);

        Debug.DrawRay(currentPosition, dirToPlayer, Color.red);

        onEnemyWalkChange?.Invoke(isWalking);

        if (hit.collider != null)
        {
            Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
            transform.Translate(avoidanceDirection * chaseSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(dirToPlayer * chaseSpeed * Time.deltaTime);
        }
    }
}