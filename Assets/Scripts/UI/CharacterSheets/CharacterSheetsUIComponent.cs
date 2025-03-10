using UnityEngine;
namespace ClickerQuest.UI.CharacterSheets
{
    public abstract class CharacterSheetsUIComponent : MonoBehaviour
    {
        protected CharacterSheetUI CharacterSheetsUI{get; private set;}

        protected virtual void OnEnable()
        {
            CharacterSheetsUI = transform.parent.GetComponent<CharacterSheetUI>();
            CharacterSheetsUI.CharacterSheetAdded += Initialize;
        }
        
        private void OnDisable()
        {
            CharacterSheetsUI.CharacterSheetAdded -= Initialize;
        }

        protected abstract void Initialize();
    }
}