using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public List<GameObject> tilePrefabs;
    public List<GameObject> obstaclePrefabs;
    public int initialTiles = 5;
    public float tileLength = 10f;
    public Transform playerTransform;
    private float spawnZ = 0f;
    private List<GameObject> activeTiles;
    private float safeZone = 15.0f;

    private void Start()
    {
        activeTiles = new List<GameObject>();
        for (int i = 0; i < initialTiles; i++)
        {
            if (i < 2)
                SpawnTile(0); // Start with two initial tiles
            else
                SpawnTile();
        }
    }

    private void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - initialTiles * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject tile;
        if (prefabIndex == -1)
            prefabIndex = Random.Range(0, tilePrefabs.Count);

        tile = ObjectPool.Instance.ReuseObject(tilePrefabs[prefabIndex].name, Vector3.forward * spawnZ, Quaternion.identity);
        if (tile != null)
        {
            activeTiles.Add(tile);
            spawnZ += tileLength;

            // Rastgele bir pozisyonda engel ekleyin
            SpawnObstacle(tile.transform);
        }
        else
        {
            Debug.LogError("Prefab not found in the pool: " + tilePrefabs[prefabIndex].name);
        }
    }

    private void SpawnObstacle(Transform parent)
    {
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Count);
        GameObject obstacle = ObjectPool.Instance.ReuseObject(obstaclePrefabs[obstacleIndex].name, parent.position + Vector3.forward * Random.Range(1, tileLength), Quaternion.identity);
        if (obstacle != null)
        {
            obstacle.transform.SetParent(parent);
        }
        else
        {
            Debug.LogError("Obstacle prefab not found in the pool: " + obstaclePrefabs[obstacleIndex].name);
        }
    }

    private void DeleteTile()
    {
        if (activeTiles.Count > 0)
        {
            GameObject tile = activeTiles[0];
            activeTiles.RemoveAt(0);
            tile.SetActive(false); // Instead of Destroy
        }
    }
}
