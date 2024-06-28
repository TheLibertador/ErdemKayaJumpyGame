using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    private Transform playerTransform; // Reference to the player to check when they pass a platform

    [SerializeField] private int platformTypeIndex = 0; // Specify the index for platform blocks in the ObjectPool
    [SerializeField] private float platformLength = 10f; // Distance between platforms
    [SerializeField] private int initialPlatforms = 7; // Number of initial platforms

    private Queue<GameObject> activePlatforms = new Queue<GameObject>(); // Queue to manage active platforms
    private float lastPlatformZPosition = 0f; // Track the last platform's Z position

    private void Start()
    { 
        if (GameManager.Instance != null)
        {
            playerTransform = GameManager.Instance.player.transform;
        }
        // Generate initial platforms
        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform(new Vector3(0, 0, i * platformLength));
        }
    }

    private void Update()
    {
        // Check if the player has passed the first platform in the queue
        if (activePlatforms.Count > 0 && playerTransform.position.z > activePlatforms.Peek().transform.position.z + platformLength)
        {
            DeactivatePlatform(activePlatforms.Dequeue()); // Return the platform to the pool
            SpawnPlatform(new Vector3(0, 0, lastPlatformZPosition + platformLength)); // Spawn a new platform
        }
    }

    private void SpawnPlatform(Vector3 position)
    {
        GameObject platform = ObjectPool.Instance.GetPooledObject(platformTypeIndex);

        if (platform != null)
        {
            platform.transform.position = position;
            platform.SetActive(true);
            activePlatforms.Enqueue(platform); // Add the platform to the queue
            lastPlatformZPosition = position.z; // Update the last platform Z position
        }
    }

    private void DeactivatePlatform(GameObject platform)
    {
        if (platform.activeInHierarchy)
        {
            platform.SetActive(false);
            ObjectPool.Instance.ReturnObjectToPool(platformTypeIndex, platform);
        }
    }
}
