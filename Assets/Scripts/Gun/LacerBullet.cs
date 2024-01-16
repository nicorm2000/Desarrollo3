using System.Collections;
using UnityEngine;

public class LacerBullet : MonoBehaviour
{
    public WeaponData weaponData;

    private float bulletSpeed;
    private float timer;
    private float currentDamage;

    private void Start()
    {
        timer = weaponData.lifespan;
        currentDamage = weaponData.damage;
        bulletSpeed = weaponData.bulletSpeed;

        StartCoroutine(TranslateBullet());
    }

    private IEnumerator TranslateBullet()
    {
        while (timer > 0f)
        {
            transform.Translate(Vector2.right * Time.deltaTime * bulletSpeed);

            timer -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.GetComponent<HealthSystem>().TakeDamage(currentDamage);

            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet_Collider"))
        {
            Destroy(gameObject);
        }
    }
}