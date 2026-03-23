using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private Dictionary<Component, Pool> pools = new Dictionary<Component, Pool>();

    [SerializeField] private int defaultSize = 10;

    private void Awake()
    {
        Instance = this;
    }

    // ✅ GENERIC SPAWN
    public static T Spawn<T>(T prefab) where T : Component
    {
        return Instance.SpawnInternal(prefab);
    }

    private T SpawnInternal<T>(T prefab) where T : Component
    {
        if (!pools.ContainsKey(prefab))
        {
            CreatePool(prefab);
        }

        var obj = pools[prefab].Get();

        return obj as T;
    }

    // overload có position
    public static T Spawn<T>(T prefab, Vector3 pos, Quaternion rot) where T : Component
    {
        var obj = Instance.SpawnInternal(prefab);
        obj.transform.SetPositionAndRotation(pos, rot);
        return obj;
    }

    public static void Despawn(Component obj)
    {
        Instance.DespawnInternal(obj);
    }

    private void DespawnInternal(Component obj)
    {
        var identity = obj.GetComponent<PoolIdentity>();

        if (identity != null && pools.ContainsKey(identity.prefab))
        {
            pools[identity.prefab].Return(obj);
        }
        else
        {
            Destroy(obj.gameObject);
        }
    }

    private void CreatePool(Component prefab)
    {
        var parent = new GameObject(prefab.name + "_Pool").transform;
        parent.SetParent(transform);

        var pool = new Pool(prefab, defaultSize, parent);
        pools.Add(prefab, pool);
    }
}
