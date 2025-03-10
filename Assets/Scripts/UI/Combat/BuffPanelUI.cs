using ClickerQuest.Characters.Buffs;
using UnityEngine;
namespace ClickerQuest.UI.Combat
{
    public class BuffPanelUI : CharacterUIComponent
    {
        [SerializeField] private BuffUI _buffUIPrefab;
        
        protected override void Initialize()
        {
            CharacterUI.CharacterInCombat.BuffsChanged += OnBuffsChanged;
        }
        
        private void OnDisable()
        {
            CharacterUI.CharacterInCombat.BuffsChanged -= OnBuffsChanged;
        }
        
        private void OnBuffsChanged()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
            foreach (Buff buff in CharacterUI.CharacterInCombat.Buffs)
                Instantiate(_buffUIPrefab, transform).Initialize(buff);
        }
    }

}