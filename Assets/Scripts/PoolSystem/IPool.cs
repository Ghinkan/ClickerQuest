using UnityEngine;
namespace PoolSystem
{
    interface IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        public T Object { get; }
        public ObjectPool<T> Pool { get; }
    }
}