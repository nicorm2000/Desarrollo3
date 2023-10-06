using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;

    private float timer;

    public WeaponData weaponData;

    private void Start()
    {
        damage = weaponData.damage;
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
            Debug.Log("Damage!");
            collision.GetComponent<HealthSystem>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet_Collider"))
        {
            Destroy(gameObject);
        }
    }
}