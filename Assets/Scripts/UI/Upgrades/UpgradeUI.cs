using AudioSystem;
using ClickerQuest.Characters.Upgrades;
using ClickerQuest.Managers;
using UnityEngine;
using UnityEngine.Events;
namespace ClickerQuest.UI.Upgrades
{
    public class UpgradeUI : MonoBehaviour
    {
        public event UnityAction UpgradeAdded;
        public event UnityAction UpgradePurchased;
        [SerializeField] private GameController _gameController;
        [SerializeField] private GoldManager _goldManager;
        [field: SerializeField] public Upgrade Upgrade {get; private set;}
        [SerializeField] private SoundData _buySoundFX;
        [SerializeField] private SoundData _errorSoundFX;

        public void Initialize(Upgrade upgrade)
        {
            Upgrade = upgrade;
            UpgradeAdded?.Invoke();
        }
        
        public void Purchase()
        {
            //TODO: Click upgrades cant be purchased if hero is not selected
            if (!_gameController.CharacterSheetUISelected)
            {
                SoundManager.Instance.CreateSoundBuilder().Play(_errorSoundFX);
                //TODO: Show Text Error
                return;
            }
            if (_goldManager.Gold >= Upgrade.Cost)
            {
                Upgrade.Apply(_gameController.CharacterSheetUISelected.Character); 
                _goldManager.RemoveGold(Upgrade.Cost);
                SoundManager.Instance.CreateSoundBuilder().Play(_buySoundFX);
                UpgradePurchased?.Invoke();
            }
            else
            {
                SoundManager.Instance.CreateSoundBuilder().Play(_errorSoundFX);
                //TODO: Show Text Error
            }
        }
    }
}