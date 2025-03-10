using System.Collections.Generic;
using ClickerQuest.Managers;
using UnityEngine;
namespace ClickerQuest.UI.CharacterSheets
{
    public class CharactersSheetsManagerUI : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        
        [SerializeField] private List<CharacterSheetUI> _sheetsList = new List<CharacterSheetUI>();

        private void OnEnable()
        {
            for (int i = 0; i < _sheetsList.Count; i++)
                _sheetsList[i].Initialize(_gameController.HeroesList[i]);
        }
    }
}