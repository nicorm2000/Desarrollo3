using System.Collections;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [Header("Bullet Dependences")]
    public WeaponData weaponData;
    public SpriteRenderer bulletSprite;

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

    private IEnumerator BulletBounce() 
    {
        if(bulletSpeed < 0) 
        {
            bulletSpeed = 0;
            bulletSpeed = weaponData.bulletSpeed;
            bulletSprite.flipX = false;
        }

        else 
        {
            bulletSpeed = 0;
            bulletSpeed = -weaponData.bulletSpeed;
            bulletSprite.flipX = true;
        }

        transform.Translate(Vector2.left * Time.deltaTime * bulletSpeed);

        yield return new WaitForSeconds(1);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.GetComponent<HealthSystem>().TakeDamage(currentDamage);

            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            collision.GetComponent<BossHealthSystem>().TakeDamage(currentDamage);

            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet_Collider"))
        {
            StartCoroutine(BulletBounce());
        }
    }
}