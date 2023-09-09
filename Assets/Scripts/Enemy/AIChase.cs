using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] private float chaseSpeed;
    private GameObject player;

    private bool isChasing = false;
    private float _distance;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isChasing = !isChasing;

            if (isChasing)
            {
                _distance = Vector2.Distance(transform.position, player.transform.position);
            }
        }

        if (isChasing)
        {
            Vector2 dir = player.transform.position - transform.position;
            dir.Normalize();

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
        }
    }
}