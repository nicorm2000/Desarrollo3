using System.Collections;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    [Header("Weapon Data Dependencies")]
    [SerializeField] private WeaponData weaponData;
    
    private float bulletSpeed;
    private float timer;
    private float currentDamage;

    [Header("Explosive Bullet")]
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject trailEffect;
    [SerializeField] private SpriteRenderer bulletSprite;
    [SerializeField] private float maxTimeToDestroy = 1f;

    private readonly float _minExplosiveArea = 0.15f;
    private readonly float _maxExplosiveArea = 1f;

    private AudioManager _audioManager;

    private void Start()
    {
        timer = weaponData.lifespan;
        currentDamage = weaponData.damage;
        bulletSpeed = weaponData.bulletSpeed;

        _audioManager = GetComponent<AudioManager>();

        sphereCollider.radius = _minExplosiveArea;
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
        sphereCollider.radius = _maxExplosiveArea;
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
            if (!AudioManager.muteSFX)
            {
                _audioManager.PlaySound(weaponData.explosion);
            }
            collision.GetComponent<HealthSystem>().TakeDamage(currentDamage);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet_Collider"))
        {
            if (!AudioManager.muteSFX)
            {
                _audioManager.PlaySound(weaponData.explosion);
            }
            StartCoroutine(StopBullet());
        }
    }
}