using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float chaseSpeed;
    private GameObject _player;

    private float _distance;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        _distance = Vector2.Distance(transform.position, _player.transform.position);

        Vector2 dir = _player.transform.position - transform.position;
        dir.Normalize();

        transform.Translate(dir * chaseSpeed * Time.deltaTime);
    }
}