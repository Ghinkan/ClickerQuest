namespace UnityEngine.Utils
{
	public class Singleton<T> : MonoBehaviour where T : Component
	{
		public static T Instance;

		protected virtual void Awake()
		{
			if (!Instance)
			{
				Instance = this as T;
				DontDestroyOnLoad(gameObject);
			}
			else
				Destroy(gameObject);
		}
	}
}