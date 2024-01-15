using System.Collections;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    public WeaponData weaponData;
    public float bulletSpeed;

    private float timer;
    private float currentDamage;

    [Header("Explosive Bullet")]
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] GameObject explosionEffect;
    
    public float maxTimerToDestroy = 2f;
    private float timerToDestroy;


    private float minExplosiveArea = 0.15f;
    private float maxExplosiveArea = 1f;

    private void Start()
    {
        timer = weaponData.lifespan;
        currentDamage = weaponData.damage;
        bulletSpeed = weaponData.bulletSpeed;

        sphereCollider.radius = minExplosiveArea;

        timerToDestroy = maxTimerToDestroy;

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
        bulletSpeed = 0f;
        sphereCollider.radius = maxExplosiveArea;
        explosionEffect.SetActive(true);
        timerToDestroy = maxTimerToDestroy;

        yield return new WaitForSeconds(1);
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