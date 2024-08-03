using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public ObjectPool obstaclePool;
    public Transform player;
    public float spawnInterval = 2f;
    public float obstacleDistance = 5f;
    public int initialObstacles = 10;
    public float[] xPositions = new float[] { -25f, 0f, 25f };
    //public float[] doorXPositions = new float[] { -32.8f, -7.8f, 17.2f };
    //public float[] rampaXPositions = new float[] { -17.2f, 7.8f, 32.8f };

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
            if (player.position.z >= nextSpawnZ - obstacleDistance * initialObstacles)
            {
                SpawnObstacle(false);
                nextSpawnZ += obstacleDistance;
            }


            for (int i = activeObstacles.Count - 1;i >= 0;i--)
            {
                if (activeObstacles[i].transform.position.z < player.position.z - 10f)
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
        int lineObstacleCount = Random.Range(1, 3);

        List<float> availableXPositions = new List<float>(xPositions);
        List<float> chosenXPositions = new List<float>();

        for (int i = 0; i < lineObstacleCount; i++)
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
                Vector3 spawnPos = new Vector3(xPosition, obstacle.transform.position.y, spawnZ);

                obstacle.transform.position = spawnPos;
                obstacle.transform.rotation = Quaternion.identity;
                obstacle.SetActive(true);

                activeObstacles.Add(obstacle);
            }
        }
    }



    /*
    private void SpawnRampa(float spawnZ, float rampX)
    {
        GameObject rampStart = obstaclePool.GetPoolObject();
        if (rampStart != null && rampStart.CompareTag("RampStart"))
        {
            rampStart.transform.position = new Vector3(rampX, rampStart.transform.position.y, spawnZ);
            rampStart.transform.rotation = Quaternion.Euler(0, -180, 0);
            rampStart.SetActive(true);
        }

        GameObject blocks = obstaclePool.GetPoolObject();
        if (blocks != null && blocks.CompareTag("WalkBlock"))
        {
            blocks.transform.position = new Vector3(rampX, blocks.transform.position.y, spawnZ + obstacleDistance);
            blocks.transform.rotation = Quaternion.identity;
            blocks.SetActive(true);
        }

        GameObject rampEnd = obstaclePool.GetPoolObject();
        if (rampEnd != null && rampEnd.CompareTag("RampEnd"))
        {
            rampEnd.transform.position = new Vector3(rampX, rampEnd.transform.position.y, spawnZ + obstacleDistance * 2);
            rampEnd.transform.rotation = Quaternion.Euler(0, 0, 0);
            rampEnd.SetActive(true);
        }
    }
    */
    /*
    private void SpawnDoor(Vector3 spawnPos)
    {
        float doorX = doorXPositions[Random.Range(0, doorXPositions.Length)];
        GameObject door = obstaclePool.GetPoolObject();
        if (door != null && door.CompareTag("DoorPrefab"))
        {
            spawnPos.x = doorX;
            door.transform.position = spawnPos;
            door.transform.rotation = Quaternion.identity;
            door.SetActive(true);
        }
    }
    */
}
