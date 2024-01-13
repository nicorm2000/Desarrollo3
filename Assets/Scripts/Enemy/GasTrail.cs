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

    private GameObject target;

    private void Start()
    {
        target = GameObject.FindWithTag("Player");

        StartCoroutine(GasCloudSpawn());
        StartCoroutine(GasCloudTrail());
    }

    private IEnumerator GasCloudSpawn()
    {
        while (true)
        {
            SpawnGasCloud();
            yield return new WaitForSeconds(timeBetweenGasSpawns);
        }
    }

    private IEnumerator GasCloudTrail()
    {
        while (true) 
        { 
        yield return new WaitForSeconds(timeBetweenDamage);
        
            if (IsPlayerInsideGas())
            {
                DealDamageToPlayer();
            }
        }
    }

    private void SpawnGasCloud()
    {
        Instantiate(gasCloud, transform.position, Quaternion.identity);

        Destroy(gasCloud, gasCloudLifespan);
    }

    private void DealDamageToPlayer()
    {
        target.GetComponent<PlayerHealth>().takeDamage(damagePerSecond);
    }

    private bool IsPlayerInsideGas()
    {
        Collider playerCollider = target.GetComponent<Collider>();
        Collider gasTrailCollider = target.GetComponent<Collider>();

        return playerCollider.bounds.Intersects(gasTrailCollider.bounds);
    }
}