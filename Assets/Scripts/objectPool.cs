using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    public static ObjectPool Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreatePool(string tag, GameObject prefab, int size)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            poolDictionary[tag] = new Queue<GameObject>();

            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                poolDictionary[tag].Enqueue(obj);
            }
        }
    }

    public GameObject ReuseObject(string tag, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(tag) && poolDictionary[tag].Count > 0)
        {
            GameObject objectToReuse = poolDictionary[tag].Dequeue();

            // Ensure the object is not destroyed before reusing
            if (objectToReuse != null)
            {
                poolDictionary[tag].Enqueue(objectToReuse);
                objectToReuse.SetActive(true);
                objectToReuse.transform.position = position;
                objectToReuse.transform.rotation = rotation;
                PoolableObject poolableObject = objectToReuse.GetComponent<PoolableObject>();
                if (poolableObject != null)
                {
                    poolableObject.OnObjectReuse();
                }
                return objectToReuse;
            }
        }
        return null;
    }
}
