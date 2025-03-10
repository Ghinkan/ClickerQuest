using TMPro;
using UnityEngine;
namespace ClickerQuest.UI.CharacterSheets
{
    public class CharacterSheetNameUI : CharacterSheetsUIComponent
    {
        [SerializeField] private TMP_Text _nameText;

        protected override void Initialize()
        {
            _nameText.text = CharacterSheetsUI.Character.Name;
        }
    }

}