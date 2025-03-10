using ClickerQuest.Characters;
using ClickerQuest.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace ClickerQuest.UI.CharacterSheets
{
    public class CharacterSheetUI : MonoBehaviour
    {
        public event UnityAction CharacterSheetAdded;
        
        [SerializeField] private GameController _gameController;
        [SerializeField] private Outline _outline;
        
        public Character Character { get; private set; }
        
        public void Initialize(Character character)
        {
            Character = character;
            Deselect();
            CharacterSheetAdded?.Invoke();
        }
        
        public void SetSelectedCharacterSheet()
        {
            if(!_gameController.HeroesUnlocked.Contains(Character))
                return;
            
            if(_gameController.CharacterSheetUISelected)
                _gameController.CharacterSheetUISelected.Deselect();
            
            _gameController.CharacterSheetUISelected = this;
            _outline.enabled = true;
        }

        private void Deselect()
        {
            _outline.enabled = false;
        }
    }
}