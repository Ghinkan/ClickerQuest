using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Utils;
namespace AudioSystem
{
    public class SoundManager : Singleton<SoundManager>
    {
        private IObjectPool<SoundEmitter> _soundEmitterPool;
        
        public readonly List<SoundEmitter> ActiveSoundEmitters = new List<SoundEmitter>();
        public readonly Queue<SoundEmitter> FrequentSoundEmitters = new Queue<SoundEmitter>();
        
        [SerializeField] private SoundEmitter _soundEmitterPrefab;
        [SerializeField] private bool _collectionCheck = true;
        [SerializeField] private int _defaultCapacity = 10;
        [SerializeField] private int _maxPoolSize = 100;
        [SerializeField] private int _maxSoundInstances = 30;

        public float TargetVolume { get; private set; } = 0.5f;
        
        private void Start() 
        {
            InitializePool();
            
            if (PlayerPrefs.HasKey("SoundVolume"))
                TargetVolume = PlayerPrefs.GetFloat("SoundVolume");
            else
                TargetVolume = 0.5f;
            
            SetVolume(TargetVolume);
        }
        
        public SoundBuilder CreateSoundBuilder() => new SoundBuilder(this);

        public bool CanPlaySound(SoundData data) 
        {
            if (!data.FrequentSound) return true;

            if (FrequentSoundEmitters.Count >= _maxSoundInstances && FrequentSoundEmitters.TryDequeue(out SoundEmitter soundEmitter)) 
            {
                try 
                {
                    soundEmitter.Stop();
                    return true;
                } 
                catch 
                {
                    Debug.Log("SoundEmitter is already released");
                }
                return false;
            }
            return true;
        }
        
        private void InitializePool() 
        {
            _soundEmitterPool = new ObjectPool<SoundEmitter>(
                CreateSoundEmitter,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                _collectionCheck,
                _defaultCapacity,
                _maxPoolSize);
        }
        
        public SoundEmitter Get() 
        {
            return _soundEmitterPool.Get();
        }

        public void ReturnToPool(SoundEmitter soundEmitter) 
        {
            _soundEmitterPool.Release(soundEmitter);
        }

        public void StopAll() 
        {
            foreach (SoundEmitter soundEmitter in ActiveSoundEmitters) 
                soundEmitter.Stop();

            FrequentSoundEmitters.Clear();
        }

        private SoundEmitter CreateSoundEmitter() 
        {
            SoundEmitter soundEmitter = Instantiate(_soundEmitterPrefab);
            soundEmitter.gameObject.SetActive(false);
            return soundEmitter;
        }
        
        private void OnTakeFromPool(SoundEmitter soundEmitter) 
        {
            soundEmitter.gameObject.SetActive(true);
            ActiveSoundEmitters.Add(soundEmitter);
        }
        
        private void OnReturnedToPool(SoundEmitter soundEmitter) 
        {
            soundEmitter.gameObject.SetActive(false);
            ActiveSoundEmitters.Remove(soundEmitter);
        }
        
        private void OnDestroyPoolObject(SoundEmitter soundEmitter) 
        {
            Destroy(soundEmitter.gameObject);
        }
        
        public void SetVolume(float newVol)
        {
            TargetVolume = newVol;
        }
    }
}
