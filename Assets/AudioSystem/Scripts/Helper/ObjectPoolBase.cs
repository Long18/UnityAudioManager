using UnityEngine;
using UnityEngine.Pool;

namespace Long18.AudioSystem.Helper
{
    public abstract class ObjectPoolBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private bool _collectionCheck = true;
        [SerializeField] private int _defaultPoolSize = 10;

        protected IObjectPool<T> _pool;

        public void Create(int poolSize = 10)
        {
            _pool = new ObjectPool<T>(OnCreateObject, OnGetObject, OnReleaseObject, OnDestroyObject,
                _collectionCheck, _defaultPoolSize, poolSize);
        }

        protected abstract void OnDestroyObject(T obj);
        protected abstract void OnReleaseObject(T obj);
        protected abstract void OnGetObject(T obj);
        protected abstract T OnCreateObject();
    }
}