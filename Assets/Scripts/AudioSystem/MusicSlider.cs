using UnityEngine;
using UnityEngine.UI;
namespace AudioSystem
{
    public class MusicSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _initVolume;
        
        private MusicManager _musicManager;

        private void Start()    
        {
            _musicManager = FindAnyObjectByType<MusicManager>();
            _initVolume = _musicManager.TargetVolume;
            _slider.SetValueWithoutNotify(_initVolume);
        }

        public void SetVolume(float newVolume)
        {
            _musicManager.SetVolume(newVolume);
            PlayerPrefs.SetFloat("MusicVolume", newVolume);
            PlayerPrefs.Save();
        }
    }
}
