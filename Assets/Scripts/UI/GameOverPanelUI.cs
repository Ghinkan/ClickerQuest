using ClickerQuest.Managers;
using UnityEngine;
namespace ClickerQuest.UI
{
    public class GameOverPanelUI : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private GameObject _panel;
        
        private void OnEnable()
        {
            _gameController.HeroesDefeated += OnHeroesDefeated;
        }
        
        private void OnDisable()
        {
            _gameController.HeroesDefeated -= OnHeroesDefeated;
        }
        
        private void OnHeroesDefeated()
        {
            _panel.SetActive(true);
        }
    }
}