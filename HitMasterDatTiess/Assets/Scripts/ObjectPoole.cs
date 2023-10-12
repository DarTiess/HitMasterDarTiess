using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class ObjectPoole<T> where T: Component
    {
        private List<T> _pool;
        private T _prefab;

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
            foreach (T obj in _pool)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }

            return null;

        }

    }
}