using System.Collections.Generic;
using ClickerQuest.Characters;
using ClickerQuest.Characters.Player;
using ClickerQuest.PersistentData;
using ClickerQuest.UI.CharacterSheets;
using UnityEngine;
using UnityEngine.EventChannels;
using UnityEngine.Events;
namespace ClickerQuest.Managers
{
    [CreateAssetMenu(fileName = "GameController", menuName = "Controllers/GameController")]
    public class GameController : ScriptableObject, IPersistentData
    {
        public event UnityAction HeroesDefeated;
        [field:SerializeField] public ClickerBehaviour MobileClickerBehaviour { get; private set; }
        [field:SerializeField] public ClickerBehaviour PCClickerBehaviour { get; private set; }
        [field:SerializeField] public ClickerBehaviour ActiveClickerBehaviour { get; private set; }
        [field:SerializeField] public List<Character> HeroesList { get; private set; }
        [field:SerializeField] public List<Character> HeroesUnlocked { get; private set; }
        
        [SerializeField] private VoidEventChannel _heroesDefeated;
        
        [HideInInspector] public CharacterSheetUI CharacterSheetUISelected;

        private void OnEnable()
        {
#if UNITY_ANDROID
            ActiveClickerBehaviour = MobileClickerBehaviour;
#else
            ActiveClickerBehaviour = PCClickerBehaviour;
#endif
            _heroesDefeated.GameEvent += OnHeroesDefeated;
        }
        
        private void OnDisable()
        {
            _heroesDefeated.GameEvent -= OnHeroesDefeated;
        }
        
        private void OnHeroesDefeated()
        {
            Debug.Log("Game Over");
            HeroesDefeated?.Invoke();
        }
        
        public void Save()
        {
            ES3.Save("Heroes Unlocked", HeroesUnlocked);
        }
        
        public void Load()
        {
            if(!ES3.KeyExists("Heroes Unlocked")) return;
            HeroesUnlocked = ES3.Load<List<Character>>("Heroes Unlocked");
        }
        
        public void Restore()
        {
            HeroesUnlocked = new List<Character>() {HeroesList[0]};
        }
    }
}