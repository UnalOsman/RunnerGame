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
    public float[] xPositions = new float[] { -31f, -6f, 19f };

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

        
        List<float> availableXPositions = new List<float>(xPositions);
        List<float> chosenXPositions=new List<float>();

        for (int i = 0;i < lineObstacleCount;i++)
        {
            if (availableXPositions.Count == 0) break;

            float randomX = availableXPositions[Random.Range(0, availableXPositions.Count)];
            chosenXPositions.Add(randomX);
            availableXPositions.Remove(randomX);
        }


        foreach (float xPosition in chosenXPositions)
        {
            GameObject obstacle = obstaclePool.GetPoolObject();
            if (obstacle != null)
            {
                float spawnZ = initial ? nextSpawnZ : player.position.z + obstacleDistance * initialObstacles;
                Vector3 spawnPos = new Vector3(xPosition,obstacle.transform.position.y,spawnZ);
                obstacle.transform.position = spawnPos;
                obstacle.transform.rotation = Quaternion.identity;
                obstacle.gameObject.SetActive(true);

                if(obstacle.CompareTag("RampStart"))
                {
                    GameObject rampStart = obstacle;
                    rampStart.transform.position = new Vector3(xPosition, rampStart.transform.position.y, spawnZ);
                    rampStart.transform.rotation=Quaternion.Euler(0,-180,0);
                    rampStart.SetActive(true);


                    GameObject blocks=obstaclePool.GetPoolObject();
                    if (blocks != null)
                    {
                        blocks.transform.position = new Vector3(xPosition, blocks.transform.position.y, spawnZ + obstacleDistance / 2);
                        blocks.transform.rotation = Quaternion.identity;
                        blocks.gameObject.SetActive(true);
                    }

                    GameObject rampEnd=obstaclePool.GetPoolObject() ;

                    if(rampEnd != null)
                    {
                        rampEnd.transform.position = new Vector3(xPosition, rampEnd.transform.position.y, spawnZ);
                        rampEnd.transform.rotation=Quaternion.Euler(0,0,0);
                        rampEnd.SetActive(true);
                    }
                }
            }
            else
            {
                Debug.Log("No pooled object available");
            }
        }
    }

}
