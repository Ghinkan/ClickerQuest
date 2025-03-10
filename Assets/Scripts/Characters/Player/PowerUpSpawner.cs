using Timers;
using UnityEngine;
namespace ClickerQuest.Characters.Player
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _clickerPowerUp;
        [SerializeField] private float _minTimeBetweenSpawns;
        [SerializeField] private float _maxTimeBetweenSpawns;
        
        private CountdownTimer _spawnTimer;

        private void Start()
        {
            _spawnTimer = new CountdownTimer(Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns));
            _spawnTimer.OnTimerStop += SpawnPowerUp;
            _spawnTimer.Start();
        }

        private void SpawnPowerUp()
        {
            Instantiate(_clickerPowerUp, transform);
            _spawnTimer.Restart(Random.Range(_minTimeBetweenSpawns, _maxTimeBetweenSpawns));
        }

        private void OnDestroy()
        {
            _spawnTimer.OnTimerStop -= SpawnPowerUp;
            _spawnTimer.Stop();
        }
    }
}