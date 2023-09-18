using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float timeBetweenSpawns = 2f;

    private ObjectPool objectPool;
    private bool isSpawning = false;

    private void Start()
    {
        objectPool = new ObjectPool(objectPrefab);
    }

    private void Update()
    {
        // Check if spawned objects have reached the target position
        foreach (var spawnedObject in objectPool.GetActiveObjects())
        {
            if (Vector3.Distance(spawnedObject.transform.position, targetPosition.position) < 0.01f)
            {
                spawnedObject.SetActive(false); // Deactivate the object
                objectPool.ReturnToPool(spawnedObject); // Return the object to the object pool
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isSpawning = !isSpawning;

            Debug.Log("Plate spawn");

            if (isSpawning)
            {
                StartCoroutine(SpawnObjects());
            }
            else
            {
                StopAllCoroutines();
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
                GameObject spawnedObject = objectPool.GetPooledObject();
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