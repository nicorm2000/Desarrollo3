using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float chaseSpeed;

    private bool isChasing = false;
    private float _distance;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Debug.Log("Damage!");
        }
    }
}