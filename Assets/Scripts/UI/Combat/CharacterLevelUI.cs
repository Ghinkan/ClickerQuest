using TMPro;
using UnityEngine;
namespace ClickerQuest.UI.Combat
{
    public class CharacterLevelUI : CharacterUIComponent
    {
        [SerializeField] private TMP_Text _levelNumber;
        
        protected override void Initialize()
        {
            _levelNumber.text = CharacterUI.CharacterInCombat.Character.Level.ToString();
        }
    }
}