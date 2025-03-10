using AudioSystem;
using UnityEngine;
using UnityEngine.Events;
namespace ClickerQuest.Combat
{
    public class Spawner : MonoBehaviour
    {
        public UnityAction Spawned;
        
        private SoundBuilder _soundBuilder;
        [SerializeField] private SoundData _spawnSound;

        private void OnEnable()
        {
            _soundBuilder ??= SoundManager.Instance.CreateSoundBuilder().WithPosition(transform.position);
            _soundBuilder.PlayDelayed(_spawnSound,0.15f);
        }
        
        public void SpawnEnemy()
        {
            gameObject.SetActive(false);
            Spawned?.Invoke();
        }

        public void DestroySpawner()
        {
            Destroy(gameObject, 0.15f);
        }
    }
}