using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace PoolSystem
{
	public class ObjectPool<T> where T : MonoBehaviour, IPoolable<T>
	{
		private readonly UnityAction<T> _pullObject;
		private readonly UnityAction<T> _pushObject;
		private readonly T _pooledObject;
		private readonly Transform _poolParent;
		
		public readonly Stack<T> PooledObjectsInPool = new Stack<T>();
		public readonly Stack<T> PooledObjectsOutOfPool = new Stack<T>();
		
		public ObjectPool(T pooledObject, Transform poolParent, int numberToSpawn = 0)
		{
			_pooledObject = pooledObject;
			_poolParent = poolParent;
			Spawn(numberToSpawn);
		}

		public ObjectPool(T pooledObject, Transform poolParent, UnityAction<T> pullObject, UnityAction<T> pushObject, int numberToSpawn = 0)
		{
			_pooledObject = pooledObject;
			_poolParent = poolParent;
			_pullObject = pullObject;
			_pushObject = pushObject;
			Spawn(numberToSpawn);
		}

		private T Spawn()
		{
			T pooledObject = GameObject.Instantiate(_pooledObject.gameObject, _poolParent).GetComponent<T>();
			pooledObject.gameObject.SetActive(false);
			pooledObject.Initialize(Push);
			return pooledObject;
		}

		private void Spawn(int numberToSpawn)
		{
			for (int i = 0; i < numberToSpawn; i++)
			{
				T pooledObject = Spawn();
				PooledObjectsInPool.Push(pooledObject);
			}
		}

		private T Pull()
		{
			if (PooledObjectsInPool.Count > 0)
				return PooledObjectsInPool.Pop();
			else
				return Spawn();
		}

		public T Pull(Vector3 position)
		{
			T pooledObject = Pull();
			PooledObjectsOutOfPool.Push(pooledObject);
			pooledObject.transform.position = position;
			
			pooledObject.gameObject.SetActive(true);
			_pullObject?.Invoke(pooledObject);
			
			return pooledObject;
		}
		
		public T Pull(Vector3 position, Quaternion rotation)
		{
			T pooledObject = Pull();
			PooledObjectsOutOfPool.Push(pooledObject);
			pooledObject.transform.SetPositionAndRotation(position, rotation);
			
			pooledObject.gameObject.SetActive(true);
			_pullObject?.Invoke(pooledObject);
			
			return pooledObject;
		}

		private void Push(T pooledObject)
		{
			PooledObjectsInPool.Push(pooledObject);
			PooledObjectsOutOfPool.Pop();
			
			_pushObject?.Invoke(pooledObject);
			
			pooledObject.gameObject.SetActive(false);
		}
	}
}