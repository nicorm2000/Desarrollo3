using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float chaseSpeed;

    public EnemyData enemyData;
    public GameObject target;
    public AnimatorUtility animator;

    public string walk = "Walk";
    public string idle = "Spawn";
    public string death = "Death";

    private void Start()
    {
        chaseSpeed = enemyData.movementSpeed;
        target = GameObject.FindWithTag("Player");

        if (animator.animations != null)
        {
            animator.PlayAnimation(walk);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) && animator.animations != null)
        {
            animator.PlayAnimation(walk);
            Debug.Log("Walk Animation");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && animator.animations != null)
        {
            animator.PlayAnimation(idle);
            Debug.Log("Idle Animation");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && animator.animations != null)
        {
            animator.PlayAnimation(death);
            Debug.Log("Death Animation");
        }

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