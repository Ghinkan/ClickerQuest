using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Utils;
namespace AudioSystem
{
    [RequireComponent(typeof(MusicManager))]
    public class MusicManager : Singleton<MusicManager>
    {
        private const float CrossFadeTime = 1.0f;
        private float _fading;
        private AudioSource _current;
        private AudioSource _previous;
        private readonly Queue<AudioClip> _playlist = new Queue<AudioClip>();
        
        [SerializeField] private List<AudioClip> _initialPlaylist;
        [SerializeField] private AudioMixerGroup _musicMixerGroup;
        
        private MusicManager _musicManager;
        public float TargetVolume { get; private set; } = 0.5f;

        private void Start() 
        {
            foreach (AudioClip clip in _initialPlaylist) 
                AddToPlaylist(clip);
            
            if (PlayerPrefs.HasKey("MusicVolume"))
                TargetVolume = PlayerPrefs.GetFloat("MusicVolume");
            else
                TargetVolume = 0.5f;
            
            SetVolume(TargetVolume);
        }

        public void AddToPlaylist(AudioClip clip) 
        {
            _playlist.Enqueue(clip);
            if (!_current && !_previous) 
                PlayNextTrack();
        }

        public void Clear() => _playlist.Clear();

        public void PlayNextTrack() 
        {
            if (_playlist.Count == 0)
                foreach (AudioClip clip in _initialPlaylist)
                    _playlist.Enqueue(clip);
            
            if (_playlist.TryDequeue(out AudioClip nextTrack))
                Play(nextTrack);
        }

        public void Play(AudioClip clip) 
        {
            if (_current && _current.clip == clip && _current.isPlaying) return;

            if (_previous&& _previous != _current) 
            {
                Destroy(_previous);
                _previous = null;
            }

            _previous = _current;

            _current = gameObject.GetOrAdd<AudioSource>();
            _current.clip = clip;
            _current.outputAudioMixerGroup = _musicMixerGroup;
            _current.loop = false;
            _current.volume = 0;
            _current.bypassListenerEffects = true;
            _current.Play();

            _fading = 0.001f;
        }

        private void Update() 
        {           
            // HandleCrossFade();

            if (_current && !_current.isPlaying)
                PlayNextTrack();
        }

        private void HandleCrossFade() 
        {
            if (_fading <= 0f) return;
            
            _fading += Time.deltaTime;

            float fraction = Mathf.Clamp01(_fading / CrossFadeTime);
            float logFraction = fraction.ToLogarithmicFraction();

            if (_previous) _previous.volume = TargetVolume - logFraction;
            if (_current) _current.volume = logFraction;

            if (fraction >= TargetVolume) 
            {
                _fading = 0.0f;
                if (_previous && _previous != _current) 
                {
                    Destroy(_previous);
                    _previous = null;
                }
            }
        }
        
        public void SetVolume(float newVolume)
        {
            TargetVolume = newVolume;
            _current.volume = newVolume;
        }
    }
}