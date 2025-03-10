using UnityEngine;
using UnityEngine.UI;
namespace ClickerQuest.UI.Combat
{
    public class HealthBarUI : CharacterUIComponent
    {
        [SerializeField] private Image _healthBar;
        
        protected override void Initialize()
        {
            CharacterUI.CharacterInCombat.Character.Health.HealthChangedPercentage += OnHealthChange;
            _healthBar.fillAmount = 1f;
        }

        private void OnDisable()
        {
            CharacterUI.CharacterInCombat.Character.Health.HealthChangedPercentage -= OnHealthChange;
        }

        private void OnHealthChange(float healthNormalized)
        {
            _healthBar.fillAmount = healthNormalized;
        }
    }
}