using UnityEngine;
namespace ClickerQuest.UI.Upgrades
{
    public abstract class UpgradeUIComponent : MonoBehaviour
    {
        protected UpgradeUI UpgradeUI;
        
        private void OnEnable()
        {
            UpgradeUI = transform.parent.GetComponent<UpgradeUI>();
            UpgradeUI.UpgradeAdded += Initialize;
        }

        private void OnDisable()
        {
            UpgradeUI.UpgradeAdded -= Initialize;

        }
        
        public abstract void Initialize();
    }

}