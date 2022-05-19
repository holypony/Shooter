using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PoolBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private T _prefab;
    private List<T> _pool;
    private bool _fixedSize = false;
    private List<T> Pool
    {
        get
        {
            if (_pool == null) throw new InvalidOperationException("You need to call InitPool before using it.");
            return _pool;
        }
        set => _pool = value;
    }

    protected List<T> InitPool(T prefab, int initial, bool fixedSize = false)
    {
        _prefab = prefab;
        Pool = new List<T>();
        _fixedSize = fixedSize;
        
        for (var i = 0; i < initial; i++)
        {
            CreateNew(Pool);
        }
        return Pool;
    }
    
    protected T Get(List<T> pool, Vector3 spawnPoint, Quaternion rotation, bool isActive = true)
    {
        foreach (var t in pool.Where(t => !t.gameObject.activeInHierarchy))
        {
            var transform1 = t.transform;
            transform1.position = spawnPoint;
            transform1.rotation = rotation;
            t.gameObject.SetActive(isActive);
            return t;
        }
        
        return CreateNew(pool, true, spawnPoint.x, spawnPoint.y, spawnPoint.z);
    }
    
    private T CreateNew(ICollection<T> pool, bool isActive = false, float x = 0, float y = 1, float z = 0)
    {
        var item = Instantiate(_prefab);

        GameObject o;
        (o = item.gameObject).SetActive(isActive);
        o.transform.position = new Vector3(x, y, z);

        pool.Add(item);
        return item;
    }
    
    protected void offAllObjects(List<T> pool)
    {
        foreach (var obj in pool)
        {
            obj.gameObject.SetActive(false);
        }
        
    }
}