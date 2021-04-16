using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObjectPool", menuName = "Object Pool", order = 1)]
public class ObjectPool : ScriptableObject
{
    [SerializeField]
    private int poolSize = 100;
    [SerializeField]
    private GameObject prefab;
    
    private Transform transform;

    Queue<GameObject> pooledObjects = new Queue<GameObject>();
    private static List<ObjectPool> pools = new List<ObjectPool>();

    private static Dictionary<Type, ObjectPool> typeToPool = new Dictionary<Type, ObjectPool>();

    public void CreatePool()
    {
        if(transform == null)
        {
            transform = new GameObject(prefab.name).GetComponent<Transform>();
        }
        else
        {
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            var go = Instantiate(prefab, transform);
            pooledObjects.Enqueue(go);
            go.SetActive(false);
        }
        pools.Add(this);
    }

    private void OnDestroy()
    {
        Destroy(transform.gameObject);
        foreach(var entry in typeToPool)
        {
            if(entry.Value == null)
            {
                typeToPool.Remove(entry.Key);
            }
        }
    }


    public T GetFromPool<T>()
    {
        if (pooledObjects.Count > 0)
        {
            var pooledObject = pooledObjects.Dequeue();
            return pooledObject.GetComponent<T>();
        }

        return Instantiate(prefab, transform).GetComponent<T>();
    }

    public static void ReturnToPool<T>(T objectToReturn)
    {
        var monoBehaviour = objectToReturn as MonoBehaviour;

        ObjectPool objectPool;

        if (typeToPool.TryGetValue(typeof(T), out objectPool))
        {
            objectPool = typeToPool[typeof(T)];
        }
        else
        {
            foreach(ObjectPool pool in pools)
            {
                T component;
                if(pool.pooledObjects.Peek().TryGetComponent(out component))
                {
                    objectPool = pool;
                    typeToPool.Add(typeof(T), pool);
                }
                
            }
        }
        
        monoBehaviour.gameObject.SetActive(false);
        monoBehaviour.gameObject.transform.SetParent(objectPool.transform);
        objectPool.pooledObjects.Enqueue(monoBehaviour.gameObject);
    }

}


