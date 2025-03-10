using ClickerQuest.Characters;
using ClickerQuest.PersistentData;
using UnityEngine;
using UnityEngine.EventChannels;
using UnityEngine.Events;
namespace ClickerQuest.Managers
{
    [CreateAssetMenu(fileName = "GoldManager", menuName = "Managers/GoldManager")]
    public class GoldManager : ScriptableObject, IPersistentData
    {
        public event UnityAction GoldChanged;
        
        [SerializeField] private CharacterEventChannel _enemyDefeated;
        
        [field: SerializeField] public int Gold { get; private set; }

        private void OnEnable()
        {
            _enemyDefeated.GameEvent += AddGold;
        }
        
        private void OnDisable()
        {
            _enemyDefeated.GameEvent -= AddGold;
        }
        
        private void AddGold(Character character)
        {
            if (character is Enemy enemy)
            {
                Gold += enemy.Gold;
                GoldChanged?.Invoke();
            }
        }

        public void AddGold(int amount)
        {
            Gold += amount;
            GoldChanged?.Invoke();
        }

        public void RemoveGold(int amount)
        {
            Gold -= amount;
            GoldChanged?.Invoke();
        }
        
        public void Save()
        {
            ES3.Save("Gold", Gold);
        }
        
        public void Load()
        {
            if(!ES3.KeyExists("Gold")) return;
            Gold = ES3.Load<int>("Gold");
        }
        
        public void Restore()
        {
            Gold = 0;
        }
    }
}