using System;
using UnityEngine;

public class AIShooterChase : MonoBehaviour
{

    [Header("Setup")]
    private bool isWalking;
    private bool _isFollowingPlayer;
    private float _nextFireTime;
    private AudioManager _audioManager;
    public event Action<bool> onShooterEnemyWalkChange;
    public float chaseSpeed;

    [Header("Referemces")]
    [SerializeField] private GameObject bullet;
    public GameObject firePoint;
    public EnemyData enemyData;
    public GameObject target;
    public HealthSystem healthSystem;
    public FlipEnemy flipEnemy;


    [Header("Timer")]
    [SerializeField] private float maxTime = 1f;
    private float _timer = 0f;


    private void Start()
    {
        target = EnemyManager.player;
        _audioManager = GetComponent<AudioManager>();
        _nextFireTime = enemyData.fireRate;

        enemyData.bullet = bullet;
        _isFollowingPlayer = enemyData.ifFollowingPlayer;
        chaseSpeed = enemyData.movementSpeed;
        _timer = maxTime;
    }

    private void Update()
    {
        if (enemyData.canChase == true) 
        {
            ShooterEnemyMovement();
        }

        if (healthSystem.dead)
        {
            onShooterEnemyWalkChange?.Invoke(false);
        }
    }

    private void ShooterEnemyMovement()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer = maxTime;
            isWalking = true;
        }

        Vector3 directionToPlayer = target.transform.position - transform.position;
        directionToPlayer.z = 0f;

        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (_isFollowingPlayer)
        {
            Vector2 playerPosition = target.transform.position;
            Vector2 currentPosition = transform.position;

            enemyData.movementDirection = (playerPosition - currentPosition).normalized;

            RaycastHit2D hit = Physics2D.Raycast(currentPosition, enemyData.movementDirection, enemyData.avoidanceDistance);

            Debug.DrawRay(currentPosition, enemyData.movementDirection, Color.red);

            onShooterEnemyWalkChange?.Invoke(isWalking);

            if (Vector3.Distance(transform.position, target.transform.position) <= enemyData.shootDistance)
            {
                _isFollowingPlayer = false;
            }

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

        else
        {
            _nextFireTime -= Time.deltaTime;

            if (_nextFireTime <= 0 && healthSystem.dead == false)
            {
                Shoot();
            }

            if (Vector3.Distance(transform.position, target.transform.position) > enemyData.shootDistance)
            {
                _isFollowingPlayer = true;
            }
        }
    }

    private void Shoot()
    {
        if (!AudioManager.muteSFX)
        {
            _audioManager.PlaySound(enemyData.projectile);
        }
        enemyData.bullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        _nextFireTime = enemyData.fireRate;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.shootDistance);
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

    public void EnemyShooterCanMove()
    {
        SetEnemyChase(true);
    }

    public void EnemyShooterCanNotMove()
    {
        SetEnemyChase(false);
    }
}