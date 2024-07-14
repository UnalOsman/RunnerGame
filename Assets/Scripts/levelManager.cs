using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class levelManager : MonoBehaviour
{
    public ObjectPool obstaclePool;
    public Transform player;

    public float spawnInterval = 2f;
    public float obstacleDistance = 5f;
    public int initialObstacles = 10;

    private float nextSpawnZ;

    private void Start()
    {
        nextSpawnZ=player.position.z + obstacleDistance;
        CreateInitialObstacles();
        StartCoroutine(SpawnObstacles());
    }

    private void CreateInitialObstacles()
    {
        for (int i = 0; i < initialObstacles; i++)
        {
            SpawnObstacle(true);
            nextSpawnZ += obstacleDistance;
        }
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {

            if(player.position.z >= nextSpawnZ - obstacleDistance*initialObstacles)
            {
                SpawnObstacle(false);
                nextSpawnZ += obstacleDistance;
            }
            
            yield return null;
        }
        
    }


    void SpawnObstacle(bool initial)
    {
        int lineObstacleCount = Random.Range(1, 3);

        
        List<float> xPositions = new List<float> { -31f, -6f, 19f };
        List<float> chosenXPositions=new List<float>();

        for (int i = 0;i < lineObstacleCount;i++)
        {
            if (xPositions.Count == 0) break;

            float randomX = xPositions[Random.Range(0, xPositions.Count)];
            chosenXPositions.Add(randomX);
            xPositions.Remove(randomX);
        }


        foreach (float xPosition in chosenXPositions)
        {
            GameObject obstacle = obstaclePool.GetPoolObject();
            if (obstacle != null)
            {
                float spawnZ = initial ? nextSpawnZ : player.position.z + obstacleDistance * initialObstacles;
                Vector3 spawnPos = new Vector3(xPosition,0,spawnZ);
                obstacle.transform.position = spawnPos;
                obstacle.transform.rotation = Quaternion.identity;
                obstacle.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("No pooled object available");
            }
        }
    }

}
