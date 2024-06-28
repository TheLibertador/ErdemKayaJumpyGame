using System.Collections.Generic;
using UnityEngine;

public class VerticalObstaclePoolManager : MonoBehaviour
{
    private Transform playerTransform; 

    [SerializeField] private int obstacleTypeIndex = 0; 
    [SerializeField] private float obstacleDistance = 20f;
    [SerializeField] private float initialZPosition = 45f;
    [SerializeField] private int initialObstacles = 7; 
    private Queue<GameObject> activeObstacles = new Queue<GameObject>(); 
    private float lastObstacleZPosition = 0f; 

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            playerTransform = GameManager.Instance.player.transform;
        }
        // Generate initial platforms
        for (int i = 0; i < initialObstacles; i++)
        {
            SpawnObstacle(new Vector3(0, 5, initialZPosition));
            initialZPosition += obstacleDistance;
        }
    }

    private void Update()
    {
        // Check if the player has passed the first platform in the queue
        if (activeObstacles.Count > 0 && playerTransform.position.z > activeObstacles.Peek().transform.position.z + obstacleDistance)
        {
            DeactivateObstacle(activeObstacles.Dequeue()); // Return the platform to the pool
            SpawnObstacle(new Vector3(0, 5, lastObstacleZPosition + obstacleDistance)); // Spawn a new platform
        }
    }

    private void SpawnObstacle(Vector3 position)
    {
        GameObject platform = ObjectPool.Instance.GetPooledObject(obstacleTypeIndex);

        if (platform != null)
        {
            platform.transform.position = position;
            platform.SetActive(true);
            activeObstacles.Enqueue(platform); // Add the platform to the queue
            lastObstacleZPosition = position.z; // Update the last platform Z position
        }
    }

    private void DeactivateObstacle(GameObject platform)
    {
        if (platform.activeInHierarchy)
        {
            platform.SetActive(false);
            ObjectPool.Instance.ReturnObjectToPool(obstacleTypeIndex, platform);
        }
    }
}
