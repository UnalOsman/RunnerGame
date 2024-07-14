using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> objectPrefabs;
    public int poolSize = 20;
    private List<GameObject> objectPool;

    private void Awake()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefabs[Random.Range(0,objectPrefabs.Count)]);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetPoolObject()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        //return null;


        
        //eðer tüm objeler aktifse yeni nesne oluþtur
        GameObject newObj = Instantiate(objectPrefabs[Random.Range(0,objectPrefabs.Count)]);
        newObj.SetActive(false);
        objectPool.Add (newObj);
        return newObj;
        
    }
}
