using UnityEngine;
using UnityEngine.UI;
namespace ClickerQuest.UI.Upgrades
{
    public class UpgradeBuyUI : UpgradeUIComponent
    {
        [SerializeField] private Button _buyButton;
        
        public override void Initialize()
        {
            _buyButton.onClick.AddListener(UpgradeUI.Purchase);
        }
    }
}