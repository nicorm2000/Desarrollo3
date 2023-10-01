using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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

        spawnCoroutine = StartCoroutine(SpawnObjects()); // Start spawning objects
    }


    private void Update()
    {
        // Check if spawned objects have reached the target position
        foreach (var objectPool in objectPools)
        {
            foreach (var spawnedObject in objectPool.GetActiveObjects())
            {
                if (Vector3.Distance(spawnedObject.transform.position, targetPosition.position) < 0.01f)
                {
                    spawnedObject.SetActive(false); // Deactivate the object
                    objectPool.ReturnToPool(spawnedObject); // Return the object to the object pool
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isSpawning = !isSpawning;

            if (!isSpawning)
            {
                Debug.Log("Stop plate spawn");

                StopCoroutine(spawnCoroutine); // Stop spawning objects
            }
            else
            {
                Debug.Log("Plate spawn");

                spawnCoroutine = StartCoroutine(SpawnObjects()); // Start spawning objects
            }
        }
    }

    private IEnumerator SpawnObjects()
    {
        while (isSpawning)
        {
            int numPlatesToSpawn = Random.Range(1, 3); // Generate a random number between 1 and 4

            for (int i = 0; i < numPlatesToSpawn; i++)
            {
                int randomIndex = Random.Range(0, objectPools.Count); // Get a random index from the objectPools list
                ObjectPool pool = objectPools[randomIndex];

                GameObject spawnedObject = pool.GetPooledObject();
                spawnedObject.transform.position = spawnPosition.position;
                spawnedObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                spawnedObject.SetActive(true);

                yield return new WaitForSeconds(timeBetweenSpawns); // Add a delay between spawns
            }

            if (Random.value < 0.5f) // Randomly skip a spawn
            {
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }
}