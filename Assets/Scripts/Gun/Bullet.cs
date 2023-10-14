using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer;

    public WeaponData weaponData;

    private void Start()
    {
        timer = weaponData.lifespan;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * weaponData.bulletSpeed);

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.GetComponent<HealthSystem>().TakeDamage(weaponData.damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet_Collider"))
        {
            Destroy(gameObject);
        }
    }
}