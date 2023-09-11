using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float attackSpeed = 10f; // Shots per second

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
        Instantiate(bullet, transform.position, transform.rotation);
        canShoot = false;
        timeBetweenShots = 1f / attackSpeed;
        Invoke(nameof(EnableShooting), timeBetweenShots);
    }

    private void EnableShooting()
    {
        canShoot = true;
    }
}