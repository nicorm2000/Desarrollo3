using System.Collections;
using UnityEngine;

public class GasTrail : MonoBehaviour
{
    [Header("Gas Cloud Configuration")]
    [SerializeField] private float timeBetweenGasSpawns;
    [SerializeField] private float damagePerSecond;
    [SerializeField] private float timeBetweenDamage;
    [SerializeField] private float trailSize;
    [SerializeField] private GameObject gasCloud;

    private GameObject target;

    private void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    private IEnumerator GasCloudSpawn()
    {
        yield return new WaitForSeconds(timeBetweenGasSpawns);
    }

    private IEnumerator GasTrailCheckRoutine()
    {
        yield return new WaitForSeconds(timeBetweenDamage);
    }

    private void SpawnGasCloud()
    {

    }

    private void DealDamageToPlayer()
    {
        target.GetComponent<PlayerHealth>().takeDamage(damagePerSecond);
    }

    private bool IsPlayerInsideGas()
    {
        return false;
    }
}