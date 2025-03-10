using ClickerQuest.Managers;
using TMPro;
using UnityEngine;
namespace ClickerQuest.UI
{
    public class GoldPanelUI : MonoBehaviour
    {
        [SerializeField] private GoldManager _goldManager;
        [SerializeField] private TMP_Text _goldText;
        
        private void OnEnable()
        {
            _goldManager.GoldChanged += UpdateGold;
            UpdateGold();
        }

        private void OnDisable()
        {
            _goldManager.GoldChanged -= UpdateGold;
        }

        private void UpdateGold()
        {
            _goldText.text = _goldManager.Gold.ToString();
        }
    }
}