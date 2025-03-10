namespace UnityEngine.RuntimeSets
{
	public class AddToGameObjectRuntimeSet : MonoBehaviour
	{
		[SerializeField] private GameObjectRuntimeSet _gameObjectRuntimeSet;

		private void OnEnable()
		{
			if(_gameObjectRuntimeSet)
				_gameObjectRuntimeSet.Add(gameObject);
		}

		private void OnDisable()
		{
			if(_gameObjectRuntimeSet)
				_gameObjectRuntimeSet.Remove(gameObject);
		}
	}
}