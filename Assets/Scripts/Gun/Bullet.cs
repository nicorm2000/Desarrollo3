using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public WeaponData weaponData;

    private float timer;
    private float currentDamage;
    private float scalingTimer = 0f;

    private void Start()
    {
        timer = weaponData.lifespan;
        currentDamage = weaponData.damage;

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
            collision.GetComponent<HealthSystem>().TakeDamage(currentDamage);

            if (!weaponData.heavyWeapon)
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
        float baseDamage = weaponData.damage;
        float initDamagePower = weaponData.initialDmgPower;
        float chargedDmgPower = weaponData.chargedDmgPower;
        float currentPower = initDamagePower;
        Vector3 chargeSize = weaponData.chargeSize;

        while (scalingTimer < chargeDuration)
        {
            float scaleRatio = scalingTimer / chargeDuration;

            currentPower = Mathf.Lerp(initDamagePower, chargedDmgPower, scaleRatio);
            currentDamage = baseDamage * currentPower;

            transform.Translate(Vector2.right * Time.deltaTime * chargeSpeed);
            transform.localScale = Vector3.Lerp(Vector3.one, chargeSize, scaleRatio);

            scalingTimer += Time.deltaTime;
            yield return null;
        }

        currentDamage = weaponData.damage;

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
        float baseDamage = weaponData.damage;
        float chargedDmgPower = weaponData.chargedDmgPower;
        float maxDmgPower = weaponData.maxDmgPower;
        float currentPower = chargedDmgPower;
        float bulletSpeed = weaponData.bulletSpeed;
        float distanceDivider = weaponData.distanceDivider;

        Vector3 chargeSize = weaponData.chargeSize;
        Vector3 shotSize = weaponData.shotSize;
        Vector3 startPos = transform.position;

        while (timer > 0f)
        {
            float bulletSpeedModified = bulletSpeed * bulletSpeedMultiplier;
            float divider = Mathf.Abs(bulletSpeed - bulletSpeed / bulletSpeedModified);
            float bulletDistance = Vector3.Distance(startPos, transform.position);

            transform.Translate(Vector2.right * Time.deltaTime * bulletSpeedModified);
            transform.localScale = Vector3.Lerp(chargeSize, shotSize, divider);

            currentPower = Mathf.Lerp(chargedDmgPower, maxDmgPower, bulletDistance / distanceDivider);

            currentDamage = baseDamage * currentPower;

            timer -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}