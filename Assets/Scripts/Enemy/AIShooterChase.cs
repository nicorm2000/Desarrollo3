using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooterChase : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float maxTime;

    private bool isFollowingPlayer;

    private float nextFireTime;

    public float chaseSpeed;
    public GameObject firePoint;

    public EnemyData enemyData;
    public PlayerData playerData;

    private void Start()
    {
        nextFireTime = maxTime;

        enemyData.bullet = bullet;
        isFollowingPlayer = enemyData.ifFollowingPlayer;
        chaseSpeed = enemyData.movementSpeed;
        playerData.model = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        // Calcula la dirección del jugador desde el objeto actual
        Vector3 directionToPlayer = playerData.model.transform.position - transform.position;
        directionToPlayer.z = 0f;  // Asegúrate de que la dirección sea plana en Z para 2D

        // Calcula el ángulo en grados y gira el objeto hacia el jugador
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (isFollowingPlayer)
        {
            Vector2 playerPosition = playerData.model.transform.position;
            Vector2 currentPosition = transform.position;

            // Calculate the direction towards the player
            Vector2 dirToPlayer = (playerPosition - currentPosition).normalized;

            // Check for collisions in front of the enemy using Raycast
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, dirToPlayer, enemyData.avoidanceDistance);

            Debug.DrawRay(currentPosition, dirToPlayer, Color.red);

            if (Vector3.Distance(transform.position, playerData.model.transform.position) <= enemyData.shootDistance)
            {
                isFollowingPlayer = false;
            }

            if (hit.collider != null)
            {
                // If a collision is detected, move away from the obstacle
                Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
                transform.Translate(avoidanceDirection * chaseSpeed * Time.deltaTime);
            }

            else
            {
                // If no collision, move towards the player
                transform.Translate(dirToPlayer * chaseSpeed * Time.deltaTime);
            }
        }

        else
        {
            nextFireTime -= Time.deltaTime;

            if (nextFireTime <= 0)
            {
                Shoot();
            }

            if (Vector3.Distance(transform.position, playerData.transform.position) > enemyData.shootDistance)
            {
                isFollowingPlayer = true;
            }
        }
    }

    private void Shoot()
    {
        enemyData.bullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        nextFireTime = maxTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.shootDistance);
    }
}
