using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> pooledObjects = new List<GameObject>();
    private GameObject objectPrefab;

    public ObjectPool(GameObject prefab)
    {
        objectPrefab = prefab;
    }

    public GameObject GetPooledObject()
    {
        // Check if there are any inactive objects in the pool
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // If no inactive objects found, create a new one
        GameObject newObj = GameObject.Instantiate(objectPrefab);

        pooledObjects.Add(newObj);

        newObj.SetActive(false);

        return newObj;
    }
}