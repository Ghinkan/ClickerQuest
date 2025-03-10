using System;
using System.Collections.Generic;
using ClickerQuest.Combat;
using UnityEngine;
using UnityEngine.EventChannels;
using UnityEngine.Utils;
namespace ClickerQuest.Characters.Skills
{
    [CreateAssetMenu(fileName = "DarkPassage", menuName = "Skill/DarkPassage")]
    public class DarkPassage : Skill
    {
        [SerializeField] private List<Character> _enemiesToSpawn = new List<Character>();
        [SerializeField] private CharactersInBattle _charactersInBattle;
        [SerializeField] private CharacterEventChannel _darkPassageUsed;
        [SerializeField] private VoidEventChannel _allSpawnsOccupied;
        [SerializeField] private GameObject _darkPassageVFX;
        [SerializeField] private int _damage;
        
        private void OnEnable()
        {
            _allSpawnsOccupied.GameEvent += DamageEffect;
        }

        private void OnDisable()
        {
            _allSpawnsOccupied.GameEvent -= DamageEffect;
        }

        private void DamageEffect()
        {
            foreach (CharacterInCombat character in _charactersInBattle.Heroes)
            {
                GameObject spawner = Instantiate(_darkPassageVFX, character.transform);
                spawner.SetActive(true);
                character.Character.Health.Decrement(_damage);
                Destroy(spawner, 1f);
            }
        }
        
        public override void Effect(CharacterInCombat characterInCombat)
        {
            Character enemyToSpawn = _enemiesToSpawn.RandomItem();
            _darkPassageUsed.RaiseEvent(enemyToSpawn);
        }
    }
}