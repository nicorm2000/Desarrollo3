using System.Collections;
using UnityEngine;

public class InkHellManager : MonoBehaviour
{
    [Header("Ink Hell Set Up")]
    [SerializeField] private Transform[] bulletSpawnPoints;

    [Header("Boss Data Dependencies")]
    [SerializeField] private BossData bossData;

    [Header("Audio Manager Dependencies")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string inkShoot;

    private ObjectPool bulletPool;
    private bool _isRotating;

    private void Start()
    {
        bulletPool = new ObjectPool(bossData.attack2Object);
    }

    public IEnumerator FireBullets()
    {
        _isRotating = true;
        StartCoroutine(RotateSpawnPoints());

        int bulletCount = 0;

        while (bulletCount < bossData.attack2MaxAmountOfRounds)
        {
            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(inkShoot);
            }
            foreach (Transform spawnPoint in bulletSpawnPoints)
            {
                GameObject bullet = bulletPool.GetPooledObject();
                bullet.GetComponent<BossBullets>().ActivateBullet(spawnPoint.position, spawnPoint.localRotation);
            }
            yield return null;
            bulletCount++;
            yield return new WaitForSeconds(bossData.attack2BulletSpawnDelay);
        }
        Debug.Log("Attack 2 Finish");
        _isRotating = false;
        StopAllCoroutines();
    }

    public IEnumerator RotateSpawnPoints()
    {
        while (_isRotating)
        {
            foreach (Transform spawnPoint in bulletSpawnPoints)
            {
                spawnPoint.Rotate(Vector3.forward, bossData.attack2BulletSpawnPointRotation);
            }
            yield return null;
        }
    }
}