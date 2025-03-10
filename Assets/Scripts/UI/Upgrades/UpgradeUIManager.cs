using System.Collections.Generic;
using ClickerQuest.Characters.Upgrades;
using UnityEngine;
using UnityEngine.Utils;
namespace ClickerQuest.UI.Upgrades
{
    public class UpgradeUIManager : MonoBehaviour
    {
        [SerializeField] private List<Upgrade> _upgrades;
        [SerializeField] private UpgradeUI _upgradeUIPrefab;
        [SerializeField] private int _numberOfUpgrades;
        
        private readonly List<UpgradeUI> _upgradeUIs = new List<UpgradeUI>();

        private void OnEnable()
        {
            RefreshShop();
        }

        private void RefreshShop()
        {
            Debug.Log("Refreshing Shop");
            ClearShop();

            for (int i = 0; i < _numberOfUpgrades; i++)
            {
                UpgradeUI upgradeUI = Instantiate(_upgradeUIPrefab, transform);
                upgradeUI.Initialize(_upgrades.RandomItem());
                _upgradeUIs.Add(upgradeUI);
                upgradeUI.UpgradePurchased += RefreshShop;
            }
        }
        
        private void ClearShop()
        {
            foreach (UpgradeUI upgradeUI in _upgradeUIs)
            {
                upgradeUI.UpgradePurchased -= RefreshShop;
                Destroy(upgradeUI.gameObject);
            }
            
            _upgradeUIs.Clear();
        }
    }
}