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
        GameObject obstacle = obstaclePool.GetPoolObject();

        if(obstacle != null )
        {
            float[] xPositions = { -31f, -6f, 19f };

            float randomX = xPositions[Random.Range(0, xPositions.Length)];

            float spawnZ=initial ? nextSpawnZ : player.position.z + obstacleDistance * initialObstacles;

            Vector3 spawnPosition = new Vector3(randomX, 0, spawnZ);

            obstacle.transform.position = spawnPosition;
            obstacle.transform.rotation = Quaternion.identity;
            obstacle.SetActive(true);

            Debug.Log("Obstacle spawned at " + spawnPosition);
        }
        else
        {
            Debug.Log("No pooled object available");
        }
    }

}
