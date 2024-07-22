using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /*
    public ObjectPool obstaclePool;
    public Transform player;

    public float spawnInterval = 2f;
    public float obstacleDistance = 5f;
    public int initialObstacles = 10;
    public float[] xPositions = new float[] { -2.5f, 0f, 2.5f };
    public float[] doorXPositions = new float[] { -32.8f, -7.8f, 17.2f };
    public float[] rampaXPositions = new float[] { -17.2f, 7.8f, 32.8f };

    private float nextSpawnZ;
    private HashSet<Vector3> usedPositions = new HashSet<Vector3>();

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

            yield return null;
        }
    }

    private void SpawnObstacle(bool initial)
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
            float spawnZ = initial ? nextSpawnZ : player.position.z + obstacleDistance * initialObstacles;
            Vector3 spawnPos = new Vector3(xPosition, 0, spawnZ);

            if (!usedPositions.Contains(spawnPos))
            {
                GameObject obstacle = obstaclePool.GetPoolObject();
                if (obstacle != null)
                {
                    if (obstacle.CompareTag("RampStart"))
                    {
                        float rampX = rampaXPositions[Random.Range(0, rampaXPositions.Length)];
                        SpawnRampa(spawnZ, rampX);
                    }
                    else if (obstacle.CompareTag("DoorPrefab"))
                    {
                        SpawnDoor(spawnZ);
                    }
                    else
                    {
                        obstacle.transform.position = spawnPos;
                        obstacle.transform.rotation = Quaternion.identity;
                        obstacle.gameObject.SetActive(true);
                    }

                    usedPositions.Add(spawnPos);
                }
                else
                {
                    Debug.Log("No pooled object available");
                }
            }
        }
    }

    private void SpawnRampa(float spawnZ, float rampX)
    {
        // Ýlk rampayý spawnla
        GameObject rampStart = obstaclePool.GetPoolObject();
        if (rampStart != null && rampStart.CompareTag("RampStart"))
        {
            rampStart.transform.position = new Vector3(rampX, rampStart.transform.position.y, spawnZ);
            rampStart.transform.rotation = Quaternion.Euler(0, -180, 0);
            rampStart.SetActive(true);
            usedPositions.Add(rampStart.transform.position);
        }

        // Bloklarý spawnla
        GameObject blocks = obstaclePool.GetPoolObject();
        if (blocks != null && blocks.CompareTag("WalkBlock"))
        {
            blocks.transform.position = new Vector3(rampX, blocks.transform.position.y, spawnZ + obstacleDistance);
            blocks.transform.rotation = Quaternion.identity;
            blocks.SetActive(true);
            usedPositions.Add(blocks.transform.position);
        }

        // Ýkinci rampayý spawnla
        GameObject rampEnd = obstaclePool.GetPoolObject();
        if (rampEnd != null && rampEnd.CompareTag("RampEnd"))
        {
            rampEnd.transform.position = new Vector3(rampX, rampEnd.transform.position.y, spawnZ + obstacleDistance * 2);
            rampEnd.transform.rotation = Quaternion.Euler(0, 0, 0);
            rampEnd.SetActive(true);
            usedPositions.Add(rampEnd.transform.position);
        }
    }

    private void SpawnDoor(float spawnZ)
    {
        float doorX = doorXPositions[Random.Range(0, doorXPositions.Length)];

        GameObject door = obstaclePool.GetPoolObject();
        if (door != null && door.CompareTag("DoorPrefab"))
        {
            Vector3 spawnPos = new Vector3(doorX, 0, spawnZ);
            door.transform.position = spawnPos;
            door.transform.rotation = Quaternion.identity;
            door.SetActive(true);

            usedPositions.Add(spawnPos);
        }
    }
    */
}
