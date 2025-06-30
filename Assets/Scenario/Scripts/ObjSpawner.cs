using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float distance = 0.05f;
    [SerializeField] private int maxObjects = 3;

    private Queue<GameObject> spawnedObjects = new Queue<GameObject>();

    public void SpawnObject()
    {
        if (objectToSpawn == null || spawnPoint == null)
        {
            Debug.LogWarning("ObjectSpawner: Missing prefab or spawn point!");
            return;
        }

        Vector3 spawnPosition = spawnPoint.position + spawnPoint.forward * distance;

        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.y = spawnPosition.y;
        Quaternion lookRotation = Quaternion.LookRotation(cameraPosition - spawnPosition);

        GameObject newObject = Instantiate(objectToSpawn, spawnPosition, lookRotation);
        spawnedObjects.Enqueue(newObject);

        if (spawnedObjects.Count > maxObjects)
        {
            GameObject oldestObject = spawnedObjects.Dequeue();
            if (oldestObject != null)
            {
                Destroy(oldestObject);
            }
        }
    }
}
