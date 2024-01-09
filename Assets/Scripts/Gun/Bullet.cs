using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public WeaponData weaponData;

    private float timer;
    private float scalingTimer = 0f;

    private void Start()
    {
        timer = weaponData.lifespan;

        if (weaponData.dopplerWeapon)
        {
            StartCoroutine(DopplerEffect());
        }
        else
        {
            StartCoroutine(TranslateBullet());
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

    private IEnumerator DopplerEffect()
    {
        float chargeSpeed = weaponData.chargeSpeed;
        float chargeDuration = weaponData.chargeDuration;
        Vector3 chargeSize = weaponData.chargeSize;

        while (scalingTimer < chargeDuration)
        {
            float scaleRatio = scalingTimer / chargeDuration;

            transform.Translate(Vector2.right * Time.deltaTime * chargeSpeed);
            transform.localScale = Vector3.Lerp(Vector3.one, chargeSize, scaleRatio);

            scalingTimer += Time.deltaTime;
            yield return null;
        }

        yield return TranslateAndScaleBullet(2f);
    }

    private IEnumerator TranslateBullet()
    {
        float bulletSpeed = weaponData.bulletSpeed;

        while (timer > 0f)
        {
            transform.Translate(Vector2.right * Time.deltaTime * bulletSpeed);

            timer -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator TranslateAndScaleBullet(float bulletSpeedMultiplier)
    {
        float bulletSpeed = weaponData.bulletSpeed;
        Vector3 chargeSize = weaponData.chargeSize;
        Vector3 shotSize = weaponData.shotSize;

        while (timer > 0f)
        {
            float bulletSpeedModified = bulletSpeed * bulletSpeedMultiplier;
            float divider = Mathf.Abs(bulletSpeed - bulletSpeed / bulletSpeedModified);

            transform.Translate(Vector2.right * Time.deltaTime * bulletSpeedModified);
            transform.localScale = Vector3.Lerp(chargeSize, shotSize, divider);

            timer -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}