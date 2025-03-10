using System.Collections;
using UnityEngine;
using UnityEngine.Utils;
namespace AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour
    {
        public SoundData Data { get; private set; }
        public AudioSource AudioSource { get; private set; }

        private Coroutine _playingCoroutine;

        private void Awake() 
        {
            AudioSource = gameObject.GetOrAdd<AudioSource>();
        }

        public void Initialize(SoundData data) 
        {
            Data = data;
            AudioSource.clip = data.Clip;
            AudioSource.outputAudioMixerGroup = data.MixerGroup;
            AudioSource.loop = data.Loop;
            AudioSource.playOnAwake = data.PlayOnAwake;
            
            AudioSource.mute = data.Mute;
            AudioSource.bypassEffects = data.BypassEffects;
            AudioSource.bypassListenerEffects = data.BypassListenerEffects;
            AudioSource.bypassReverbZones = data.BypassReverbZones;
            
            AudioSource.priority = data.Priority;
            AudioSource.volume = data.Volume;
            AudioSource.pitch = data.Pitch;
            AudioSource.panStereo = data.PanStereo;
            AudioSource.spatialBlend = data.SpatialBlend;
            AudioSource.reverbZoneMix = data.ReverbZoneMix;
            AudioSource.dopplerLevel = data.DopplerLevel;
            AudioSource.spread = data.Spread;
            
            AudioSource.minDistance = data.MinDistance;
            AudioSource.maxDistance = data.MaxDistance;
            
            AudioSource.ignoreListenerVolume = data.IgnoreListenerVolume;
            AudioSource.ignoreListenerPause = data.IgnoreListenerPause;
            
            AudioSource.rolloffMode = data.RolloffMode;
        }
        
        public void Play() 
        {
            if (_playingCoroutine != null) 
                StopCoroutine(_playingCoroutine);
            
            AudioSource.Play();
            _playingCoroutine = StartCoroutine(WaitForSoundToEnd());
        }

        private IEnumerator WaitForSoundToEnd() 
        {
            yield return new WaitWhile(() => AudioSource.isPlaying);
            Stop();
        }

        public void Stop()
        {
            if (_playingCoroutine != null) 
            {
                StopCoroutine(_playingCoroutine);
                _playingCoroutine = null;
            }
            
            AudioSource.Stop();
            SoundManager.Instance.ReturnToPool(this);
        }

        public void WithRandomPitch(float min = -0.05f, float max = 0.05f) 
        {
            AudioSource.pitch += Random.Range(min, max);
        }
    }
}