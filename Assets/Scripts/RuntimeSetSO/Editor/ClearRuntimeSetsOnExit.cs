using UnityEditor;
namespace UnityEngine.RuntimeSets.Editor
{
	[InitializeOnLoad]
	public static class ClearRuntimeSetsOnExit
	{
		// Constructor
		static ClearRuntimeSetsOnExit()
		{
			// Register the OnPlayModeStateChange method just once when initializing 
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private static void OnPlayModeStateChanged(PlayModeStateChange stateChange)
		{
			if (stateChange == PlayModeStateChange.ExitingPlayMode)
			{
				// Locate all GameObjectRuntimeSetSO asset IDs
				string[] guids = AssetDatabase.FindAssets("t:GameObjectRuntimeSetSO");
				
				foreach (string guid in guids)
				{
					// Locate the asset by ID and path
					string path = AssetDatabase.GUIDToAssetPath(guid);
					Debug.Log(AssetDatabase.GUIDToAssetPath(guid));

					GameObjectRuntimeSet runtimeSet = AssetDatabase.LoadAssetAtPath<GameObjectRuntimeSet>(path);

					// Clear the Items list in each runtime set
					if (runtimeSet)
						runtimeSet.Clear();
				}
			}
		}
	}
}