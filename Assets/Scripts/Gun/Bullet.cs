using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public WeaponData weaponData;

    private float scalingDuration = 0.25f;
    private float timer;
    private bool isDopplerWeapon;
    private bool isScaling = true;
    private float scalingTimer = 0f;

    private void Start()
    {
        timer = weaponData.lifespan;
        isDopplerWeapon = weaponData.dopplerWeapon;

        if (weaponData.dopplerWeapon)
        {
            StartCoroutine(DopplerEffect());
        }
    }

    private void Update()
    {
        if (!weaponData.dopplerWeapon)
        {
            //All of the Update should execute after the coroutine finishes
            transform.Translate(Vector2.right * Time.deltaTime * weaponData.bulletSpeed);

            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                Destroy(gameObject);
            }
        }
        else if (!isScaling)
        {
            transform.Translate(Vector2.right * Time.deltaTime * weaponData.bulletSpeed);

            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                Destroy(gameObject);
            }
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
        while (scalingTimer < scalingDuration)
        {
            transform.Translate(Vector2.right * Time.deltaTime * 2f);
            float scaleRatio = scalingTimer / scalingDuration;
            transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(1, 10, 1), scaleRatio);

            scalingTimer += Time.deltaTime;
            yield return null;
        }

        isScaling = false;
    }
}