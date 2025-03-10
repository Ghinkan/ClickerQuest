using AudioSystem;
using ClickerQuest.Characters.Player;
using ClickerQuest.PersistentData;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Utils;
namespace ClickerQuest.Characters
{
    [CreateAssetMenu(fileName = "Hero", menuName = "Characters/Hero")]
    public class Hero : Character, IPersistentData
    {
        public override Character SelectRandomObjective()
        {
            if (CharactersInBattle.Enemies.Count <= 0)
                return null;
            
            Character enemyAttacked = CharactersInBattle.Enemies.RandomItem().Character;
            Debug.Log($"{name} Attack {enemyAttacked.name}");
            return enemyAttacked;
        }
        
        public override void Clicked(ClickerStats clickerStats)
        {
            Health.Increment(clickerStats.ClickHeal.ActualValue);
            SoundManager.Instance.CreateSoundBuilder().Play(clickerStats.HealSound);

        }
        
        [Button]
        public void Save()
        {
            ES3.Save($"{name} CharacterStats", Stats);
        }
        
        [Button]
        public void Load()
        {
            if(!ES3.KeyExists($"{name} CharacterStats")) return;
            RestoreLevels();
            SetStats(ES3.Load<Stats>($"{name} CharacterStats"));
        }
        
        public void Restore()
        {
            RestoreLevels();
        }
    }
}