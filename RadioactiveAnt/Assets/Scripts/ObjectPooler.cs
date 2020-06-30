using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //CREATE POOL IN RESOURCES FOLDER TO FUNCTION.
    //Assets/Resources/Pools/CreatePoolHere

    private List<ScriptablePool> Pools = new List<ScriptablePool>();
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        InstanceManager<ObjectPooler>.ResetInstances();
        InstanceManager<ObjectPooler>.CreateInstance("ObjectPooler", this);

        Object[] ScriptablePools = Resources.LoadAll("Pools", typeof(ScriptablePool));
        foreach (ScriptablePool pool in ScriptablePools)
        {
            //Check if pool info is filled.
            if(pool.Amount > 0 && pool.Prefab != null && pool.Tag != null)
            {
                Pools.Add(pool);
            }
            else 
            {
                Debug.LogWarning("Pool: " + pool.name + " is missing some information. \n Please go back to Resources/Pools and fill in the information correctly");
            }
        }
    }

    // Create pools and put them in empty gameObjects to make sure the hierarchy window is clean.
    private void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        if(Pools.Count < 1)
        {
            return;
        }
        foreach (ScriptablePool pool in Pools)
        {
            if(!PoolDictionary.ContainsKey(pool.Tag))
            {
                GameObject containerObject = new GameObject(pool.Tag + "Pool");
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.Amount; i++)
                {
                    GameObject obj = Instantiate(pool.Prefab, containerObject.transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
                PoolDictionary.Add(pool.Tag, objectPool);
            }
        }
    }

    //Spawn an object from the corresponding pool with the given variables
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = PoolDictionary[tag].Dequeue();

        if(objectToSpawn == null)
        {
            print("object is null");
        }
        objectToSpawn.SetActive(false);
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        PoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
