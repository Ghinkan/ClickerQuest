using UnityEngine;
using UnityEngine.UI;
namespace ClickerQuest.UI.Combat
{
    public class SkillButtonUI : CharacterUIComponent
    {
        [SerializeField] private Button _skillButton;
        [SerializeField] private Image _skillButtonImage;
        [SerializeField] private Color _buttonEnableColor;
        [SerializeField] private Color _buttonDisabledColor;
        
        protected override void Initialize()
        {
            _skillButtonImage.sprite = CharacterUI.CharacterInCombat.Character.Skill.SkillIcon;
            
            _skillButton.onClick.AddListener(CharacterUI.CharacterInCombat.UseSkill);
            CharacterUI.CharacterInCombat.SkillReady += ActivateSkillButton;
            CharacterUI.CharacterInCombat.SkillUsed += DeactivateSkillButton;
            
            DeactivateSkillButton();
        }

        private void OnDisable()
        {
            CharacterUI.CharacterInCombat.SkillReady -= ActivateSkillButton;
            CharacterUI.CharacterInCombat.SkillUsed -= DeactivateSkillButton;
        }
        
        private void DeactivateSkillButton()
        {
            _skillButton.interactable = false;
            _skillButton.image.color = _buttonDisabledColor;
        }

        private void ActivateSkillButton()
        {
            _skillButton.interactable = true;
            _skillButton.image.color = _buttonEnableColor;
        }
        
        public void UseSkill()
        {
            CharacterUI.CharacterInCombat.UseSkill();
        }
    }
}