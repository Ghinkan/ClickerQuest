using UnityEngine;
namespace AudioSystem
{
    [CreateAssetMenu(fileName = "AudioSpawner", menuName = "AudioSpawner")]
    public class AudioSpawner : ScriptableObject
    {
        [SerializeField] private SoundManager _soundManager;
        [SerializeField] private MusicManager _musicManager;
        private static AudioSpawner _instance;
        private static AudioSpawner Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = Resources.Load<AudioSpawner>("Controllers/AudioSpawner");
                    if (!_instance)
                        Debug.LogError("AudioController asset not found in Resources/Controllers.");
                }
                return _instance;
            }
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnStartGame()
        {
            Instantiate(Instance._soundManager, Vector3.zero, Quaternion.identity);
            Instantiate(Instance._musicManager, Vector3.zero, Quaternion.identity);
        }
    }
}