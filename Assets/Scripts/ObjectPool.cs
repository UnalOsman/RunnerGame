using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public GameObject[] obstaclePrefabs;

    public int poolSize = 10;
    private List<GameObject> objectPool;

    void Start()
    {
        objectPool = new List<GameObject>();

        foreach (var prefab in obstaclePrefabs)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj=Instantiate(prefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            
        }
    }

    public GameObject GetPoolObject()
    {
        if(objectPool.Count > 0)
        {
            for (int i = 0; i < objectPool.Count; i++)
            {
                int index = Random.Range(0, objectPool.Count);
                if (!objectPool[index].activeInHierarchy)
                {
                    return objectPool[index];
                }
            }
        }
        return null;
    }

    public void ReturnPoolObject(GameObject obj)
    {
        obj.SetActive(false);
        //objectPool.Remove(obj);
    }
}
