using System.Collections;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    public WeaponData weaponData;
    
    private float bulletSpeed;
    private float timer;
    private float currentDamage;

    [Header("Explosive Bullet")]
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject trailEffect;
    [SerializeField] SpriteRenderer bulletSprite;
    
    public float maxTimeToDestroy = 1f;


    private float minExplosiveArea = 0.15f;
    private float maxExplosiveArea = 1f;

    private void Start()
    {
        timer = weaponData.lifespan;
        currentDamage = weaponData.damage;
        bulletSpeed = weaponData.bulletSpeed;

        sphereCollider.radius = minExplosiveArea;
        trailEffect.SetActive(true);
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

    private IEnumerator StopBullet() 
    {
        trailEffect.SetActive(false);
        bulletSpeed = 0f;
        sphereCollider.radius = maxExplosiveArea;
        explosionEffect.SetActive(true);
        bulletSprite.sprite = null;

        yield return new WaitForSeconds(maxTimeToDestroy);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            StartCoroutine(StopBullet());
            collision.GetComponent<HealthSystem>().TakeDamage(currentDamage);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet_Collider"))
        {
            StartCoroutine(StopBullet());
        }
    }
}