using UnityEngine;
using UnityEngine.UI;
namespace ClickerQuest.UI.CharacterSheets
{
    public class CharacterSheetImageUI : CharacterSheetsUIComponent
    {
        [SerializeField] private Image _image;
        
        protected override void Initialize()
        {
            _image.sprite = CharacterSheetsUI.Character.Portrait;
        }
    }

}