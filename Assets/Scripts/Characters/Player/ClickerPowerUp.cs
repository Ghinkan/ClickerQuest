using AudioSystem;
using ClickerQuest.Managers;
using Timers;
using UnityEngine;
namespace ClickerQuest.Characters.Player
{
    public class ClickerPowerUp : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _timeActive;
        [SerializeField] private SoundData _spawnSound;
        [SerializeField] private SoundData _activeSound;
        
        private CountdownTimer _timer;

        private void OnEnable()
        {
            _timer = new CountdownTimer(_timeActive);
            _timer.Start();
            SoundManager.Instance.CreateSoundBuilder().Play(_spawnSound);
            _timer.OnTimerStop += () => _animator.SetTrigger("Despawn");
        }

        public void Effect()
        {
            Debug.Log("PowerUp clicked");
            SoundManager.Instance.CreateSoundBuilder().Play(_activeSound);
            _gameController.ActiveClickerBehaviour.ActivatePowerUp();
            _timer.Stop();
        }
        
        private void Despawn()
        {
            Destroy(gameObject);
        }
    }
}