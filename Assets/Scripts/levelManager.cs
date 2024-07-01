using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class levelManager : MonoBehaviour
{
    public ObjectPool obstaclePool;
    public Transform spawnPoint;

    public float spawnInterval = 2f;
    public float obstacleDistance = 5f;

    private void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    public IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(spawnInterval);
        }
        
    }


    void SpawnObstacle()
    {
        GameObject obstacle = obstaclePool.GetPoolObject();

        if(obstacle != null )
        {
            obstacle.transform.position = spawnPoint.position + new Vector3(Random.Range(-obstacleDistance, obstacleDistance), 0, 0);
            obstacle.transform.rotation = spawnPoint.rotation;
            obstacle.SetActive(true);
            Debug.Log("Obstacle spawned");
        }
        else
        {
            Debug.Log("No pooled object available");
        }
    }

}
