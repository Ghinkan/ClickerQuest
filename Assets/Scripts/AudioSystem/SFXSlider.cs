using UnityEngine;
using UnityEngine.UI;
namespace AudioSystem
{
    public class SFXSlider : MonoBehaviour
    {
        private SoundManager _soundManager;
        [SerializeField] private SoundData _testSound;
        [SerializeField] private Slider _slider;
        [SerializeField] private float _initVolume;

        private float _timer;
        [SerializeField] private float _timeToPlaySound = 0.5f;
        
        private SoundBuilder _soundBuilder;
    
        private void Start()
        {
            _soundManager = FindAnyObjectByType<SoundManager>();
            _initVolume = _soundManager.TargetVolume;
            _slider.SetValueWithoutNotify(_initVolume);
            _soundBuilder = _soundManager.CreateSoundBuilder();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }
    
        public void SetVolume(float newVolume)
        {
            _soundManager.SetVolume(newVolume);
            
            PlayerPrefs.SetFloat("SoundVolume", newVolume);
            PlayerPrefs.Save();
        
            if (_timer >= _timeToPlaySound)
            {
                _soundBuilder.Play(_testSound);
                _timer = 0f;
            }
        }
    }
}