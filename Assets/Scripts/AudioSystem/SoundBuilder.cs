using Timers;
using UnityEngine;
namespace AudioSystem
{
    public class SoundBuilder
    {
        private readonly SoundManager _soundManager;
        private Vector3 _position = Vector3.zero;
        private bool _randomPitch;

        public SoundBuilder(SoundManager soundManager) 
        {
            _soundManager = soundManager;
        }

        public SoundBuilder WithPosition(Vector3 position) 
        {
            _position = position;
            return this;
        }

        public SoundBuilder WithRandomPitch() 
        {
            _randomPitch = true;
            return this;
        }

        public void Play(SoundData soundData) 
        {
            if (soundData == null) 
            {
                Debug.LogError("SoundData is null");
                return;
            }
            
            if (!_soundManager.CanPlaySound(soundData)) return;
            
            SoundEmitter soundEmitter = _soundManager.Get();
            soundEmitter.Initialize(soundData);
            soundEmitter.transform.position = _position;
            soundEmitter.transform.parent = _soundManager.transform;
            soundEmitter.AudioSource.volume = _soundManager.TargetVolume;

            if (_randomPitch) 
                soundEmitter.WithRandomPitch();
            
            if (soundData.FrequentSound) 
                _soundManager.FrequentSoundEmitters.Enqueue(soundEmitter);
            
            soundEmitter.Play();
        }

        public void PlayDelayed(SoundData soundData, float delay)
        { 
            CountdownTimer timer = new CountdownTimer(delay);
            timer.Start();
            timer.OnTimerStop += () => Play(soundData);
        }
    }
}