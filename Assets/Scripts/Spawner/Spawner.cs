using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private int amountToSpawn;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject spawnIndicator;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    public IEnumerator SpawnObjects()
    {
        Debug.Log("Spawning!");
        yield return new WaitForSeconds(spawnTime);
        Debug.Log("Spawn time reached!");

        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, GetRandomSpawnPosition(), Quaternion.identity);
            //Here will be the animation for the object to spawn

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        if (spawnPositions.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPositions.Length);
            return spawnPositions[randomIndex].position;
        }
        else
        {
            return transform.position;
        }
    }
}