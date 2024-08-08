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
            for (int i = 0; i < 10; i++)
            {
                if (player.position.z >= nextSpawnZ - 2 * obstacleDistance)
                {
                    SpawnObstacle(false);
                    nextSpawnZ += player.transform.position.z + obstacleDistance;
                    Debug.Log("SpawnObstacles ilk if koþulu çalýþtý." + nextSpawnZ);
                }
            }


            for (int i = activeObstacles.Count - 1; i >= 0; i--)
            {
                if (activeObstacles[i].transform.position.z < player.position.z - 10f)
                {
                    obstaclePool.ReturnPoolObject(activeObstacles[i]);
                    activeObstacles.RemoveAt(i);
                    Debug.Log("SpawnObstacles ikinci if koþulu çalýþtý." + nextSpawnZ);
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
                float spawnZ = initial ? nextSpawnZ + 80 : nextSpawnZ + obstacleDistance;
                Vector3 spawnPos = new Vector3(xPosition, obstacle.transform.position.y, spawnZ);

                obstacle.transform.position = spawnPos;
                obstacle.transform.rotation = Quaternion.identity;
                obstacle.SetActive(true);

                activeObstacles.Add(obstacle);
                availableXPositions.Add(xPosition);
            }
        }
    }

}
