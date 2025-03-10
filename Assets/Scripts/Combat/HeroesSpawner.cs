using System.Collections.Generic;
using System.Linq;
using ClickerQuest.Characters;
using ClickerQuest.Managers;
using UnityEngine;
namespace ClickerQuest.Combat
{
    public class HeroesSpawner : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private CharactersInBattle _charactersInBattle;
        [SerializeField] private CharacterInCombat _characterInCombatPrefab;
        [SerializeField] private List<Transform> _spawnsTransforms = new List<Transform>();
        
        private void Start()
        {
            _charactersInBattle.ClearHeroes();
            SpawnHeroes();
        }
        
        private void SpawnHeroes()
        {
            foreach (Character hero in _gameController.HeroesUnlocked)
            {
                hero.SetStats(hero.Stats);
                CharacterInCombat characterInCombat = Instantiate(_characterInCombatPrefab, FirstEmptyAvailableSpawn());
                characterInCombat.Initialize(hero);
                _charactersInBattle.AddHero(characterInCombat);
            }
        }
        
        private Transform FirstEmptyAvailableSpawn()
        {
            return _spawnsTransforms.FirstOrDefault(spawn => spawn.childCount == 0);
        }
    }
}