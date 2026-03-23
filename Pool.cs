using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Component prefab;
    private Queue<Component> objects = new Queue<Component>();
    private Transform parent;

    public Pool(Component prefab, int size, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < size; i++)
        {
            var obj = Create();
            obj.gameObject.SetActive(false);
            objects.Enqueue(obj);
        }
    }

    private Component Create()
    {
        var obj = Object.Instantiate(prefab, parent);

        var identity = obj.gameObject.AddComponent<PoolIdentity>();
        identity.prefab = prefab;

        return obj;
    }

    public Component Get()
    {
        Component obj;

        if (objects.Count == 0)
        {
            obj = Create();
        }
        else
        {
            obj = objects.Dequeue();
        }

        obj.gameObject.SetActive(true);

        if (obj is IPoolable poolable)
            poolable.OnSpawn();

        return obj;
    }

    public void Return(Component obj)
    {
        if (obj is IPoolable poolable)
            poolable.OnDespawn();

        obj.gameObject.SetActive(false);
        obj.transform.SetParent(parent);
        objects.Enqueue(obj);
    }
}
