using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float lerpSpeed = 1f;
    [SerializeField] private float despawnDelay = 1f;

    private ObjectPool objectPool;

    private void Start()
    {
        objectPool = new ObjectPool(objectPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        GameObject spawnedObject = objectPool.GetPooledObject();
        spawnedObject.transform.position = spawnPosition.position;
        spawnedObject.SetActive(true);

        StartCoroutine(LerpObject(spawnedObject));
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