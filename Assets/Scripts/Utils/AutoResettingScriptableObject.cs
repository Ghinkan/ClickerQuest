using Sirenix.OdinInspector;
namespace UnityEngine.Utils
{
	public abstract class AutoResettingScriptableObject : SerializedScriptableObject
	{
		//TODO:Refactor, Maybe do attribute?
#if UNITY_EDITOR
		[System.NonSerialized] private string _initialState;
		[System.NonSerialized] private bool _isPlaying;

		private void OnEnable()
		{
			if (string.IsNullOrEmpty(_initialState)) _initialState = JsonUtility.ToJson(this);

			UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
			UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private void OnValidate()
		{
			if (!_isPlaying) _initialState = JsonUtility.ToJson(this);

			UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
			UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange state)
		{
			_isPlaying = state != UnityEditor.PlayModeStateChange.EnteredEditMode;
			if (!_isPlaying) ResetToInitialState();
		}

		private void ResetToInitialState()
		{
			if (!string.IsNullOrEmpty(_initialState)) JsonUtility.FromJsonOverwrite(_initialState, this);
		}
#endif
	}
}