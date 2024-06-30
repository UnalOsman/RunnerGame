using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject objectPrefab;
    public int poolSize = 10;
    private List<GameObject> objectPool;

    private void Awake()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj=Instantiate(objectPrefab);
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



        //eðer tüm objeler aktifse yeni nesne oluþtur
        GameObject newObj=Instantiate(objectPrefab);
        newObj.SetActive(false);
        objectPool.Add (newObj);
        return newObj;
    }
}
