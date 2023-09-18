using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float lerpSpeed = 1f;
    [SerializeField] private float despawnDelay = 1f;
    [SerializeField] private float timeBetweenSpawns = 1f;

    private ObjectPool objectPool;
    private bool isSpawning = false;

    private void Start()
    {
        objectPool = new ObjectPool(objectPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpawning = !isSpawning;

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
            int numPlatesToSpawn = Random.Range(1, 5); // Generate a random number between 1 and 4

            for (int i = 0; i < numPlatesToSpawn; i++)
            {
                GameObject spawnedObject = objectPool.GetPooledObject();
                spawnedObject.transform.position = spawnPosition.position;
                spawnedObject.SetActive(true);

                StartCoroutine(LerpObject(spawnedObject));
            }

            yield return new WaitForSeconds(timeBetweenSpawns);

            if (Random.value < 0.5f) // Randomly skip a spawn
            {
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }

    private IEnumerator LerpObject(GameObject obj)
    {
        float lerpTime = 0f;
        Vector3 startPosition = obj.transform.position;

        while (lerpTime < 1f)
        {
            lerpTime += Time.deltaTime * lerpSpeed;
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition.position, lerpTime);
            yield return null;
        }

        yield return new WaitForSeconds(despawnDelay);

        obj.SetActive(false);
    }
}