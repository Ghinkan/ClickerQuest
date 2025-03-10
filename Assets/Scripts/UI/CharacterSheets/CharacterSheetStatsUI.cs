using TMPro;
using UnityEngine;
namespace ClickerQuest.UI.CharacterSheets
{
    public class CharacterSheetStatsUI : CharacterSheetsUIComponent
    {
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _maxHealth;
        [SerializeField] private TMP_Text _attack;
        [SerializeField] private TMP_Text _attackRate;
        [SerializeField] private TMP_Text _skillRate;
        
        protected override void Initialize()
        {
            RefreshValues();
            CharacterSheetsUI.Character.CharacterLevelUp += RefreshValues;
        }
        
        private void OnDisable()
        {
            CharacterSheetsUI.Character.CharacterLevelUp -= RefreshValues;
        }
        
        private void RefreshValues()
        {
            _level.text = CharacterSheetsUI.Character.Level.ToString();
            _maxHealth.text = CharacterSheetsUI.Character.Stats.MaxHealth.ActualValue.ToString();
            _attack.text = CharacterSheetsUI.Character.Stats.Attack.ActualValue.ToString();
            _attackRate.text = CharacterSheetsUI.Character.Stats.AttackRate.ActualValue.ToString();
            _skillRate.text = CharacterSheetsUI.Character.Stats.SkillRate.ActualValue.ToString();
        }
    }
}