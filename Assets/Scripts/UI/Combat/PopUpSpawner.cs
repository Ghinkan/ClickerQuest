using ClickerQuest.Characters;
using UnityEngine;
using UnityEngine.Utils;
namespace ClickerQuest.UI.Combat
{
    public class PopUpSpawner : MonoBehaviour
    {
        [SerializeField] private Color _damageColor;
        [SerializeField] private Color _healColor;
        [SerializeField] private Color _goldColor;
        [SerializeField] private float _goldYOffset;
        
        private Character _character;
        private PopUpPool _popUpPool;

        public void Initialize(Character character)
        {
            _character = character;
            _popUpPool = PopUpPool.Instance;
            _character.Health.HealthChanged += HealthChanged;
            _character.Health.Died += CharacterDied;
        }
        
        private void OnDisable()
        {
            _character.Health.HealthChanged -= HealthChanged;
            _character.Health.Died -= CharacterDied;
        }

        private void HealthChanged(int amount)
        {
            if (amount <= 0)
            {
                PopUpNumber popUpNumber = _popUpPool.Pool.Pull(transform.position.Set(z: PopUpPool.Instance.transform.position.z));
                popUpNumber.Setup(amount, _damageColor);
            }
            else
            {
                PopUpNumber popUpNumber = _popUpPool.Pool.Pull(transform.position.Set(z: PopUpPool.Instance.transform.position.z));
                popUpNumber.Setup(amount, _healColor);
            }
        }
        
        private void CharacterDied()
        {
            if (_character is Enemy enemy)
            {
                PopUpNumber popUpNumber = _popUpPool.Pool.Pull(transform.position.Set(y:_goldYOffset, z: PopUpPool.Instance.transform.position.z));
                popUpNumber.Setup(enemy.Gold, _goldColor);
            }
        }
    }
}