using ClickerQuest.Managers;
using UnityEngine;
namespace ClickerQuest.Characters.Player
{
    public class Clicker : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;

        private void Start()
        {
            _gameController.ActiveClickerBehaviour.Initialize();
        }

        private void Update()
        {
            _gameController.ActiveClickerBehaviour.ClickBehaviour();
        }
    }
}