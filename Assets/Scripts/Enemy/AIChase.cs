using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float chaseSpeed;

    public EnemyData enemyData;
    public GameObject target;

    private void Start()
    {
        chaseSpeed = enemyData.movementSpeed;
        target = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        Vector2 playerPosition = target.transform.position;
        Vector2 currentPosition = transform.position;

        Vector2 dirToPlayer = (playerPosition - currentPosition).normalized;

        RaycastHit2D hit = Physics2D.Raycast(currentPosition, dirToPlayer, enemyData.avoidanceDistance);

        Debug.DrawRay(currentPosition, dirToPlayer, Color.red);

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