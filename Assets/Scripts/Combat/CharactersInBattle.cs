using System.Collections.Generic;
using ClickerQuest.Characters;
using UnityEngine;
using UnityEngine.EventChannels;
using UnityEngine.Utils;
namespace ClickerQuest.Combat
{
    [CreateAssetMenu(fileName = "CharactersInBattle", menuName = "CharactersInBattle")]
    public class CharactersInBattle : AutoResettingScriptableObject
    {
        [field: SerializeField] public List<CharacterInCombat> Heroes { get; private set; }
        [field: SerializeField] public List<CharacterInCombat> Enemies { get; private set; }
        [SerializeField] private CharacterEventChannel _characterDefeated;
        [SerializeField] private VoidEventChannel _heroesDefeated;

        private void OnEnable()
        {
            _characterDefeated.GameEvent += RemoveCharacter;
        }

        private void OnDisable()
        {
            _characterDefeated.GameEvent -= RemoveCharacter;
        }
        
        private void RemoveCharacter(Character character)
        {
            if(character is Enemy)
                RemoveEnemy(character);
            else
                RemoveHero(character);
        }
        
        public void AddHero(CharacterInCombat hero)
        {
            Heroes.Add(hero);
        }

        private void RemoveHero(Character hero)
        {
            for (int i = 0; i < Heroes.Count; i++)
                if(Heroes[i].Character == hero)
                    Heroes.Remove(Heroes[i]);

            if (Heroes.Count <= 0)
            {
                foreach (CharacterInCombat enemy in Enemies)
                    enemy.EndCombat();
                
                _heroesDefeated.RaiseEvent();
            }
        }

        public void AddEnemy(CharacterInCombat enemy)
        {
            Enemies.Add(enemy);
        }

        private void RemoveEnemy(Character enemy)
        {
            for (int i = 0; i < Enemies.Count; i++)
                if (Enemies[i].Character == enemy)
                    Enemies.RemoveAt(i);
        }

        public void ClearEnemies()
        {
            Enemies.Clear();
        }

        public void ClearHeroes()
        {
            Heroes.Clear();
        }
    }
}