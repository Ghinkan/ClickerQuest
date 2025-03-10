using UnityEngine;
using UnityEngine.UI;
namespace ClickerQuest.UI.Combat
{
    public class ManaBarUI : CharacterUIComponent
    {
        [SerializeField] private Image _manaBar;

        protected override void Initialize()
        {
            _manaBar.fillAmount = 0f;
        }

        private void Update()
        {
            _manaBar.fillAmount = 1f - CharacterUI.CharacterInCombat.SkillTimer.Progress;
        }
    }

}