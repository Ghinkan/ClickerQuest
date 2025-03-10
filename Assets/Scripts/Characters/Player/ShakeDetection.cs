using System.Collections.Generic;
using ClickerQuest.Combat;
using ClickerQuest.Managers;
using UnityEngine;
namespace ClickerQuest.Characters.Player
{
    public class ShakeDetection : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private ClickerStats _clickerStats;
        [SerializeField] private CharactersInBattle _charactersInBattle;
        [SerializeField] private float _shakeThreshold = 2.0f;
        [SerializeField] private float _shakeMultiplier = 1.0f;
        [SerializeField] private float _shakeCooldown = 0.5f;

        private float _shakeTimer;
        private Vector3 _lastAcceleration;

        private void Start()
        {
            if (_gameController.ActiveClickerBehaviour is PCClickerBehaviour)
                _shakeCooldown = 0.15f;
            
            _lastAcceleration = Input.acceleration;
        }

        private void Update()
        {
            if (_gameController.ActiveClickerBehaviour is MobileClickerBehaviour)
                MobileShakeDetection();
            else
                PCShakeDetection();
        }

        private void MobileShakeDetection()
        {
            Vector3 acceleration = Input.acceleration;
            Vector3 deltaAcceleration = Input.acceleration - _lastAcceleration;
            
            if (deltaAcceleration.sqrMagnitude >= _shakeThreshold)
            {
                _shakeTimer += deltaAcceleration.sqrMagnitude * _shakeMultiplier * Time.deltaTime;

                if (_shakeTimer >= _shakeCooldown)
                {
                    ExecuteShakeAction();
                    _shakeTimer = 0f;
                }
            }
            else
                _shakeTimer = Mathf.Max(0f, _shakeTimer - Time.deltaTime);
            
            _lastAcceleration = acceleration;
        }
        
        private void PCShakeDetection()
        {
            _shakeTimer += Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                if (_shakeTimer >= _shakeCooldown)
                {
                    ExecuteShakeAction();
                    _shakeTimer = 0f;
                }
            }
        }

        private void ExecuteShakeAction()
        {
            Debug.Log(_gameController.ActiveClickerBehaviour.PowerUpActive);
            if(!_gameController.ActiveClickerBehaviour.PowerUpActive) return;
            
            List<CharacterInCombat> heroesToClick = new List<CharacterInCombat>(_charactersInBattle.Heroes);
            List<CharacterInCombat> enemiesToClick = new List<CharacterInCombat>(_charactersInBattle.Enemies);
            
            if (_charactersInBattle.Heroes.Count > 0)
                foreach (CharacterInCombat hero in heroesToClick)
                    hero.Clicked(_clickerStats);

            if (_charactersInBattle.Enemies.Count > 0)
                foreach (CharacterInCombat enemy in enemiesToClick)
                    enemy.Clicked(_clickerStats);
            
            Debug.Log("Shake detected!");
        }
    }
}