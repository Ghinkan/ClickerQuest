using TMPro;
using UnityEngine;
namespace ClickerQuest.UI.Upgrades
{
    public class UpgradeDescriptionUI : UpgradeUIComponent
    {
        [SerializeField] private TMP_Text _upgradeDescription;
        
        public override void Initialize()
        {
            _upgradeDescription.text = UpgradeUI.Upgrade.Description;
        }
    }
}