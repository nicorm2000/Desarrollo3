using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifespan;

    private float timer;

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * bulletSpeed);

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        timer = lifespan;
    }
}