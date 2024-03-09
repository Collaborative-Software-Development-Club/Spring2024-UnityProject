using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject spawnedObject;
    public int number;
    public Transform parentObject;
    public float minX = 240f;
    public float maxX = 315f;
    public float minY = -2f;
    public float maxY = 0.5f;
    public float minZ = 650f;
    public float maxZ = 750f;
    public float respawnInterval = 120f; // seconds

    void Start()
    {
        PlaceObject();
        StartCoroutine(CheckAndRespawn());
    }

    void PlaceObject()
    {
        if (parentObject == null)
        {
            parentObject = GameObject.Find("ObjectSpawner").transform;
        }

        if (parentObject == null)
        {
            Debug.LogError("Parent not found for spawning objects");
            return;
        }

        for (int i = 0; i < number; i++)
        {
            // Instantiate the object with the specified parent
            GameObject newObject = Instantiate(spawnedObject, GeneratedPosition(), Quaternion.identity, parentObject);
        }
    }

    Vector3 GeneratedPosition()
    {
        float x, y, z;
        x = UnityEngine.Random.Range(minX, maxX);
        y = UnityEngine.Random.Range(minY, maxY);
        z = UnityEngine.Random.Range(minZ, maxZ);
        return new Vector3(x, y, z);
    }

    IEnumerator CheckAndRespawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnInterval);

            // Check if any spawned objects are missing (destroyed)
            int currentObjectCount = parentObject.childCount;

            if (currentObjectCount < number)
            {
                int objectsToRespawn = number - currentObjectCount;

                for (int i = 0; i < objectsToRespawn; i++)
                {
                    GameObject newObject = Instantiate(spawnedObject, GeneratedPosition(), Quaternion.identity, parentObject);
                }
            }
        }
    }
}
