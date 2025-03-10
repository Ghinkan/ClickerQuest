using UnityEngine;
using UnityEngine.UI;
namespace ClickerQuest.UI.Upgrades
{
    public class UpgradeImageUI : UpgradeUIComponent
    {
        [SerializeField] private Image _image;

        public override void Initialize()
        {
            _image.sprite = UpgradeUI.Upgrade.Sprite;
        }
    }

}