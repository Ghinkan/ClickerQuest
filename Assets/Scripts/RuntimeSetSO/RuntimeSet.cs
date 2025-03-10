using System.Collections.Generic;
using UnityEngine.EventChannels;
namespace UnityEngine.RuntimeSets
{
	public abstract class RuntimeSet<T> : ScriptableObject where T: MonoBehaviour
	{
		// Event for the Editor script 
		public System.Action ItemsChanged;

		[Header("Optional")]
		[Tooltip("Notify other objects this Runtime Set has changed")]
		[SerializeField] private VoidEventChannel _runtimeSetUpdated;
		
		// Use the Items to track a list of T at runtime.
		public List<T> Items { get; } = new();

		private void OnEnable()
		{
			ItemsChanged?.Invoke();
		}
		
		// Adds a specific item to the Runtime Set's Items list.
		public void Add(T thing)
		{
			if (!Items.Contains(thing))
				Items.Add(thing);
			
			if (_runtimeSetUpdated)
				_runtimeSetUpdated.RaiseEvent();

			ItemsChanged?.Invoke();
		}

		// Removes a specific item from the Runtime Set's Items list.
		public void Remove(T thing)
		{
			if (Items.Contains(thing))
				Items.Remove(thing);
			
			if (_runtimeSetUpdated)
				_runtimeSetUpdated.RaiseEvent();

			ItemsChanged?.Invoke();
		}
		
		// Reset the list
		public void Clear()
		{
			Items.Clear();

			if (_runtimeSetUpdated)
				_runtimeSetUpdated.RaiseEvent();

			ItemsChanged?.Invoke();
		}
		
		// Clean up any items after the list is cleared
		public void DestroyItems()
		{
			foreach (T item in Items)
				Destroy(item.gameObject);
		}
	}
}