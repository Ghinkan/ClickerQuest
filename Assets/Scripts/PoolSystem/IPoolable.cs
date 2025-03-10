using UnityEngine.Events;
namespace PoolSystem
{
	public interface IPoolable<T>
	{
		void Initialize(UnityAction<T> returnAction);
		void ReturnToPool();
	}
}