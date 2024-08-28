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
                if (activeObstacles[i].transform.position.z < player.position.z - 50f)
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

        GameObject obstacle = obstaclePool.GetPoolObject();

        if (obstacle.transform.localScale.z <= 20)
        {
            foreach (float xPosition in chosenXPositions)
            {
                
                if (obstacle != null)
                {
                    GameObject olusanObje=FindClosestObstacle(obstacle) ;
                    bool canSpawn = true;
                    float spawnZ = initial ? nextSpawnZ + obstacleDistance : nextSpawnZ;

                    if (obstacle.CompareTag("Obstacle"))
                    {

                        float distance = Mathf.Abs(olusanObje.transform.position.z - obstacle.transform.position.z);
                        float distanceNext = Mathf.Abs(obstacle.transform.position.z - spawnZ);
                        if (distance < 2*obstacleDistance + obstacle.transform.localScale.z && distanceNext < obstacleDistance + obstacle.transform.localScale.z)
                        {
                            canSpawn = false;
                            break;
                        }
                        spawnZ = initial ? nextSpawnZ + obstacleDistance : nextSpawnZ;
                        spawnZ = canSpawn ? spawnZ:spawnZ + obstacleDistance*2f;
                    }
                    else if (obstacle.CompareTag("Block"))
                    {
                        float distance = Mathf.Abs(olusanObje.transform.position.z - obstacle.transform.position.z);
                        float distanceNext = Mathf.Abs(obstacle.transform.position.z - spawnZ);
                        if (distance < 2 * obstacleDistance + obstacle.transform.localScale.z && distanceNext < 2 * obstacleDistance + obstacle.transform.localScale.z)
                        {
                            canSpawn = false;
                            break;
                        }
                        spawnZ = initial ? nextSpawnZ + obstacleDistance * 3 : nextSpawnZ + obstacleDistance * 2;
                        spawnZ = canSpawn ? spawnZ : spawnZ + obstacleDistance * 2f;
                    }
                    else if (obstacle.CompareTag("WalkBlock"))
                    {
                        float distance = Mathf.Abs(olusanObje.transform.position.z - obstacle.transform.position.z);
                        float distanceNext = Mathf.Abs(obstacle.transform.position.z - spawnZ);
                        if (distance < 3 * obstacleDistance + obstacle.transform.localScale.z && distanceNext < 3 * obstacleDistance + obstacle.transform.localScale.z)
                        {
                            canSpawn = false;
                            break;
                        }
                        spawnZ = initial ? nextSpawnZ + obstacleDistance * 5 : nextSpawnZ + obstacleDistance * 4;
                        spawnZ=canSpawn ? spawnZ :spawnZ + obstacleDistance * 5;
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

    GameObject FindClosestObstacle(GameObject currentObstacle)
    {
        GameObject closestObstacle= null;

        float closestDistance = float.PositiveInfinity;

        foreach(GameObject obstacle in activeObstacles)
        {
            if (currentObstacle == obstacle) continue; // eðer eþitse döngüde kalan koda devam et.

            float distance=Vector3.Distance(currentObstacle.transform.position, obstacle.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObstacle = obstacle;
            }
        }

        return closestObstacle;
    }

}
