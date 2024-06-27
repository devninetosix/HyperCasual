using UnityEngine;
using UnityEngine.Pool;


namespace PadakPadak
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance { get; private set; }

        public Pipes objectToPool;
        public int initialPoolSize = 5;

        private ObjectPool<Pipes> _pool;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _pool = new ObjectPool<Pipes>(
                createFunc: () =>
                {
                    Pipes obj = Instantiate(objectToPool, transform, true);
                    obj.gameObject.SetActive(false);
                    return obj;
                },
                actionOnGet: obj => obj.gameObject.SetActive(true),
                actionOnRelease: obj => obj.gameObject.SetActive(false),
                actionOnDestroy: obj => Destroy(obj.gameObject),
                collectionCheck: false,
                defaultCapacity: initialPoolSize,
                maxSize: 10
            );

            // Pre-warm the pool
            for (int i = 0; i < initialPoolSize; i++)
            {
                Pipes obj = _pool.Get();
                _pool.Release(obj);
            }
        }

        public Pipes GetPooledObject()
        {
            return _pool.Get();
        }

        public void ReleasePooledObject(Pipes obj)
        {
            _pool.Release(obj);
        }
    }
}