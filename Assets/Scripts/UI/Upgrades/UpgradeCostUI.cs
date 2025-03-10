using TMPro;
using UnityEngine;
namespace ClickerQuest.UI.Upgrades
{
    public class UpgradeCostUI : UpgradeUIComponent
    {
        [SerializeField] private TMP_Text _costText;
        
        public override void Initialize()
        {
            _costText.text = UpgradeUI.Upgrade.Cost.ToString();
        }
    }

}