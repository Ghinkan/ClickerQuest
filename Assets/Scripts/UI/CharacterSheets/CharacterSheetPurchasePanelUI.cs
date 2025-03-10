using AudioSystem;
using ClickerQuest.Managers;
using TMPro;
using UnityEngine;
namespace ClickerQuest.UI.CharacterSheets
{
    public class CharacterSheetPurchasePanelUI : CharacterSheetsUIComponent
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private GoldManager _goldManager;
        [SerializeField] private TMP_Text _characterPrice;
        [SerializeField] private SoundData _buySoundFX;
        [SerializeField] private SoundData _errorSoundFX;
        
        protected override void Initialize()
        {
            if(_gameController.HeroesUnlocked.Contains(CharacterSheetsUI.Character))
                gameObject.SetActive(false);
            
            _characterPrice.text = CharacterSheetsUI.Character.Price.ToString();
        }
        
        public void Purchase()
        {
            if (_goldManager.Gold >= CharacterSheetsUI.Character.Price)
            {
                _gameController.HeroesUnlocked.Add(CharacterSheetsUI.Character);
                _goldManager.RemoveGold(CharacterSheetsUI.Character.Price);
                SoundManager.Instance.CreateSoundBuilder().Play(_buySoundFX);
                gameObject.SetActive(false);
            }
            else
            {
                SoundManager.Instance.CreateSoundBuilder().Play(_errorSoundFX);
                //TODO: Show Text Error
            }
        }
    }
}