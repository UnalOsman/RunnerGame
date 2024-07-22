using UnityEngine;

public class PoolInitializer : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject[] obstaclePrefabs;
    public int poolSize = 5;

    private void Start()
    {
        foreach (var prefab in tilePrefabs)
        {
            ObjectPool.Instance.CreatePool(prefab.name, prefab, poolSize);
        }
        foreach (var prefab in obstaclePrefabs)
        {
            ObjectPool.Instance.CreatePool(prefab.name, prefab, poolSize);
        }
    }
}
