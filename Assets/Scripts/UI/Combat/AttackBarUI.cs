using UnityEngine;
using UnityEngine.UI;
namespace ClickerQuest.UI.Combat
{
    public class AttackBarUI : CharacterUIComponent
    {
        [SerializeField] private Image _attackBar;
        
        protected override void Initialize()
        {
            _attackBar.fillAmount = 0f;
        }
        
        private void Update()
        {
            _attackBar.fillAmount = 1f - CharacterUI.CharacterInCombat.AttackTimer.Progress;
        }
    }
}