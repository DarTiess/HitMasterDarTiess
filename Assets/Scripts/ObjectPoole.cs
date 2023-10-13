using System.Collections.Generic;
using UnityEngine;

public class ObjectPoole<T> where T: Component
{
    private List<T> _pool;
    private T _prefab;
    private int _indexInPool=0;

    public ObjectPoole()
    {
        _pool = new List<T>();
    }

    public void CreatePool(T prefab, int size, Transform parent)
    {
        _prefab = prefab;

        for (int i = 0; i < size; i++)
        {
            var obj = Object.Instantiate(_prefab, parent, true);
            obj.gameObject.SetActive(false);
            _pool.Add(obj);
        }
    }

    public T GetObject()
    {
        while (_pool[_indexInPool].gameObject.activeInHierarchy)
        {
            _indexInPool+=1;
            if (_indexInPool >= _pool.Count)
            {
                _indexInPool = 0;
            }
        }
        _pool[_indexInPool].gameObject.SetActive(true);
        return _pool[_indexInPool];
    }

}