using System.Collections;
using UnityEngine;

public class GasTrail : MonoBehaviour
{
    [Header("Gas Cloud Configuration")]
    [SerializeField] private float timeBetweenGasSpawns;
    [SerializeField] private float damagePerSecond;
    [SerializeField] private float timeBetweenDamage;
    [SerializeField] private float trailSize;
    [SerializeField] private float gasCloudLifespan;
    [SerializeField] private GameObject gasCloud;

    [Header("Health System Dependencies")]
    [SerializeField] private HealthSystem healthSystem;

    [Header("Audio Manager Dependencies")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] string gasSFX;

    private GameObject target;
    private ObjectPool gasCloudPool;

    private void Start()
    {
        target = EnemyManager.player;
        gasCloudPool = new ObjectPool(gasCloud);
        StartCoroutine(GasCloudSpawn());
        StartCoroutine(GasCloudTrail());
    }

    private IEnumerator GasCloudSpawn()
    {
        while (!healthSystem.dead)
        {
            SpawnGasCloud();
            if (!AudioManager.muteSFX)
            {
                audioManager.PlaySound(gasSFX);
            }
            else if (AudioManager.muteSFX)
            {
                audioManager.StopSpecificSound(gasSFX, gameObject);
            }

            yield return new WaitForSeconds(timeBetweenGasSpawns);
        }
    }

    private IEnumerator GasCloudTrail()
    {
        while (!healthSystem.dead) 
        { 
        yield return new WaitForSeconds(timeBetweenDamage);

            if (IsPlayerInsideGasTrail())
            {
                DealDamageToPlayer();
            }
        }
    }

    private void SpawnGasCloud()
    {
        GameObject gasCloud = gasCloudPool.GetPooledObject();
        gasCloud.transform.position = transform.position;
        gasCloud.SetActive(true);
        StartCoroutine(ReturnGasCloudToPool(gasCloud));
    }

    private IEnumerator ReturnGasCloudToPool(GameObject gasCloud)
    {
        yield return new WaitForSeconds(gasCloudLifespan);

        gasCloud.SetActive(false);
        gasCloudPool.ReturnToPool(gasCloud);
    }

    private void DealDamageToPlayer()
    {
        target.GetComponent<PlayerHealth>().takeDamage(damagePerSecond);
    }

    private bool IsPlayerInsideGasTrail()
    {
        Collider playerCollider = target.GetComponent<Collider>();

        foreach (GameObject gasCloud in gasCloudPool.GetActiveObjects())
        {
            Collider gasCloudCollider = gasCloud.GetComponent<Collider>();

            if (playerCollider.bounds.Intersects(gasCloudCollider.bounds))
            {
                return true;
            }
        }

        return false;
    }

    public void SpawnFinalGasCloud(float duration)
    {
        GameObject finalGasCloud = Instantiate(gasCloud, transform.position, Quaternion.identity);

        StartCoroutine(DestroyFinalGasCloud(finalGasCloud, duration));
    }

    private IEnumerator DestroyFinalGasCloud(GameObject finalGasCloud, float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(finalGasCloud);
    }
}