using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class LevelManager : MonoBehaviour
{
    public ObjectPool obstaclePool;
    public Transform player;
    public float spawnInterval = 2f;
    public float obstacleDistance = 5f;
    public int initialObstacles = 10;
    public float spawnDistance = 800f;
    public float[] xPositions = new float[] { -25f, 0f, 25f };

    private float nextSpawnZ;
    private List<GameObject> activeObstacles = new List<GameObject>();

    private void Start()
    {
        nextSpawnZ = player.position.z + obstacleDistance;
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

            if (player.position.z <= nextSpawnZ + spawnDistance)
            {
                nextSpawnZ += obstacleDistance;
                SpawnObstacle(false);
                Mathf.Max(nextSpawnZ,player.position.z + spawnDistance);
            }





            for (int i = activeObstacles.Count - 1; i >= 0; i--)
            {
                if (activeObstacles[i].transform.position.z < player.position.z - 20f)
                {
                    obstaclePool.ReturnPoolObject(activeObstacles[i]);
                    activeObstacles.RemoveAt(i);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    void SpawnObstacle(bool initial)
    {
        Random rand = new Random();
        int lineObstacleCount = rand.Next(1,3);

        List<float> availableXPositions = new List<float>(xPositions);
        List<float> chosenXPositions = new List<float>();

        for (int i = 0; i < lineObstacleCount; i++)
        {
            if (availableXPositions.Count == 0) break;
            float randomX = availableXPositions[rand.Next(0, availableXPositions.Count)];
            chosenXPositions.Add(randomX);
            availableXPositions.Remove(randomX);
        }

        foreach (float xPosition in chosenXPositions)
        {
            GameObject obstacle = obstaclePool.GetPoolObject();
            if (obstacle != null)
            {

                float zScale=obstacle.transform.localScale.z;
                float spawnZ = nextSpawnZ;

                if(obstacle.CompareTag("Obstacle"))
                {
                    spawnZ = initial ? nextSpawnZ + obstacleDistance : nextSpawnZ;
                }
                else if(obstacle.CompareTag("Block"))
                {
                    spawnZ=initial ? nextSpawnZ + obstacleDistance * 3 :nextSpawnZ + obstacleDistance * 2;
                }
                else if(obstacle.CompareTag("WalkBlock"))
                {
                   spawnZ = initial ? nextSpawnZ + obstacleDistance * 5 : nextSpawnZ + obstacleDistance * 4;
                }

                if (zScale >= obstacleDistance * 3)
                {
                    nextSpawnZ += zScale + obstacleDistance;
                }

                Vector3 spawnPos = new Vector3(xPosition, obstacle.transform.position.y, spawnZ);

                obstacle.transform.position = spawnPos;
                obstacle.transform.rotation = Quaternion.identity;
                obstacle.SetActive(true);

                activeObstacles.Add(obstacle);
                //availableXPositions.Add(xPosition);
            }
        }
    }

}
