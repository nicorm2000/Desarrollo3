using UnityEngine;

public class AIShooterChase : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    private bool isFollowingPlayer;

    private float nextFireTime;

    public float chaseSpeed;
    public GameObject firePoint;

    public EnemyData enemyData;
    public GameObject target;

    public HealthSystem healthSystem;

    private void Start()
    {
        nextFireTime = enemyData.fireRate;

        enemyData.bullet = bullet;
        isFollowingPlayer = enemyData.ifFollowingPlayer;
        chaseSpeed = enemyData.movementSpeed;
        target = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Vector3 directionToPlayer = target.transform.position - transform.position;
        directionToPlayer.z = 0f;

        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        if (isFollowingPlayer)
        {
            Vector2 playerPosition = target.transform.position;
            Vector2 currentPosition = transform.position;

            Vector2 dirToPlayer = (playerPosition - currentPosition).normalized;

            RaycastHit2D hit = Physics2D.Raycast(currentPosition, dirToPlayer, enemyData.avoidanceDistance);

            Debug.DrawRay(currentPosition, dirToPlayer, Color.red);

            if (Vector3.Distance(transform.position, target.transform.position) <= enemyData.shootDistance)
            {
                isFollowingPlayer = false;
            }

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

        else
        {
            nextFireTime -= Time.deltaTime;

            if (nextFireTime <= 0 && healthSystem._dead == false)
            {
                Shoot();
            }

            if (Vector3.Distance(transform.position, target.transform.position) > enemyData.shootDistance)
            {
                isFollowingPlayer = true;
            }
        }
    }

    private void Shoot()
    {
        enemyData.bullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        nextFireTime = enemyData.fireRate;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.shootDistance);
    }
}