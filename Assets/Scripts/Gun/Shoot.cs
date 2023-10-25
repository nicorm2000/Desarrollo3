using UnityEngine;

public class Shoot : MonoBehaviour
{
    public WeaponData weaponData;

    private float timeBetweenShots;
    private bool canShoot = true;

    private void Update()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        if (weaponData.isShootWeapon == true) 
        {
            Instantiate(weaponData.bulletPrefab, transform.position, transform.rotation);
            canShoot = false;
            timeBetweenShots = 1f / weaponData.attackSpeed;
            Invoke(nameof(EnableShooting), timeBetweenShots);
        }
    }

    private void EnableShooting()
    {
        canShoot = true;
    }
}