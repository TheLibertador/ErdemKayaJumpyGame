using System.Collections.Generic;
using UnityEngine;

public class DiamondPoolManager : MonoBehaviour
{
    public int diamondTypeIndex; 
    private Transform playerTransform; 
    [SerializeField] public float minZDistance = 10f; 
    [SerializeField] public float maxZDistance = 30f; 
    [SerializeField] public float minYPosition = 9.75f;
    [SerializeField] public float maxYPosition = 14.0f;
    [SerializeField] public int initialDiamonds = 7;

    private Queue<GameObject> activeDiamonds = new Queue<GameObject>();
    private float lastDiamondZPosition = 0f;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            playerTransform = GameManager.Instance.player.transform;
        }
        // Generate initial diamonds
        for (int i = 0; i < initialDiamonds; i++)
        {
            SpawnDiamond(GetRandomZPosition(), GetRandomYPosition());
        }
    }

 
    private void SpawnDiamond(float zPosition, float yPosition)
    {
        GameObject diamond = ObjectPool.Instance.GetPooledObject(diamondTypeIndex);

        if (diamond != null)
        {
            Vector3 position = new Vector3(0, yPosition, zPosition);
            diamond.transform.position = position;
            diamond.SetActive(true);
            activeDiamonds.Enqueue(diamond); 
            lastDiamondZPosition = zPosition; 
        }
    }

    public void DeactivateDiamond(GameObject diamond)
    {
        if (diamond.activeInHierarchy)
        {
            diamond.SetActive(false);
            ObjectPool.Instance.ReturnObjectToPool(diamondTypeIndex, diamond);
            SpawnDiamond(GetRandomZPosition(), GetRandomYPosition());
        }
    }

    private float GetRandomZPosition()
    {
        return lastDiamondZPosition + Random.Range(minZDistance, maxZDistance);
    }

    private float GetRandomYPosition()
    {
        return Random.Range(minYPosition, maxYPosition);
    }
}
