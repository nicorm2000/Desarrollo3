using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectPrefabs;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float timeBetweenSpawns = 2f;

    private List<ObjectPool> objectPools;
    private bool isSpawning = true;
    private Coroutine spawnCoroutine;

    private void Start()
    {
        objectPools = new List<ObjectPool>();

        foreach (GameObject prefab in objectPrefabs)
        {
            ObjectPool objectPool = new ObjectPool(prefab);
            objectPools.Add(objectPool);
        }

        spawnCoroutine = StartCoroutine(SpawnObjects());
    }


    private void Update()
    {
        foreach (var objectPool in objectPools)
        {
            foreach (var spawnedObject in objectPool.GetActiveObjects())
            {
                if (Vector3.Distance(spawnedObject.transform.position, targetPosition.position) < 1f)
                {
                    spawnedObject.SetActive(false);
                    objectPool.ReturnToPool(spawnedObject);
                }
            }
        }
    }

    private IEnumerator SpawnObjects()
    {
        while (isSpawning)
        {
            int numPlatesToSpawn = Random.Range(1, 3);

            for (int i = 0; i < numPlatesToSpawn; i++)
            {
                int randomIndex = Random.Range(0, objectPools.Count);
                ObjectPool pool = objectPools[randomIndex];

                GameObject spawnedObject = pool.GetPooledObject();
                spawnedObject.transform.position = spawnPosition.position;
                spawnedObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                spawnedObject.SetActive(true);

                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            if (Random.value < 0.5f)
            {
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }
}