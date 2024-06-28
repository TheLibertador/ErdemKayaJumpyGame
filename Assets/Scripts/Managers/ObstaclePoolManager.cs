using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] platforms;
    [SerializeField] private int obstacleType1Index = 0; // Index for obstacle type 1 in ObjectPool
    [SerializeField] private int obstacleType2Index = 1; // Index for obstacle type 2 in ObjectPool

    private float startX = 0f; // Predetermined x position
    private float startY = 5f; // Predetermined y position
    private float startZ = 0f; // Predetermined z position

    private float zSpacing = 10f; // Spacing between obstacles on z axis

    private void Start()
    {
        GenerateObstacles();
    }

    private void GenerateObstacles()
    {
        float currentZ = startZ;

        for (int i = 0; i < platforms.Length - 1; i++)
        {
            Transform currentPlatform = platforms[i];
            Transform nextPlatform = platforms[i + 1];

            // Randomly choose an obstacle type index
            int chosenObstacleIndex = Random.value > 0.5f ? obstacleType1Index : obstacleType2Index;

            // Get a pooled obstacle object
            GameObject obstacle = ObjectPool.Instance.GetPooledObject(chosenObstacleIndex);

            if (obstacle != null)
            {
                // Calculate position
                float randomY = Random.Range(startY, startY + 5f); // Adjust y range as needed

                // Set the position of the obstacle
                Vector3 position = new Vector3(startX, randomY, currentZ);
                obstacle.transform.position = position;

                // Activate the obstacle
                obstacle.SetActive(true);

                // Update current Z position for the next obstacle
                currentZ += zSpacing;
            }
            else
            {
                Debug.LogWarning("No pooled objects available for index: " + chosenObstacleIndex);
            }
        }
    }
}
