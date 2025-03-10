using AudioSystem;
using ClickerQuest.PersistentData;
using Sirenix.OdinInspector;
using Stats;
using UnityEngine;
namespace ClickerQuest.Characters.Player
{
    [CreateAssetMenu(fileName = "ClickerStats", menuName = "ClickerStats")]
    public class ClickerStats : ScriptableObject, IPersistentData
    {    
        [field:SerializeField] public int Level { get; private set; }
        
        [field:SerializeField]
        [field:InlineProperty]
        [field:HideLabel]
        [field:BoxGroup(nameof(ClickDamage), centerLabel: true)] 
        public Stat ClickDamage { get; private set; }
        
        [field:SerializeField]
        [field:InlineProperty]
        [field:HideLabel]
        [field:BoxGroup(nameof(ClickHeal), centerLabel: true)] 
        public Stat ClickHeal { get; private set; }
        
        [field:SerializeField] public SoundData DamageSound { get; private set; }
        [field:SerializeField] public SoundData HealSound { get; private set; }
        [field:SerializeField] public SoundData BlockedDamageSound { get; private set; }

        public void LevelUp()
        {
            Level++;
        }

        private void SetStats(Stat clickDamage, Stat clickHeal)
        {
            ClickDamage = clickDamage;   
            ClickHeal = clickHeal;
        }
        
        [Button]
        public void RestoreLevels()
        {
            ClickDamage.RemoveAllModifiers();
            ClickHeal.RemoveAllModifiers();
        }
        
        [Button]
        public void Save()
        {
            ES3.Save($"{name} ClickDamage", ClickDamage);
            ES3.Save($"{name} ClickHeal", ClickHeal);
        }
        
        [Button]
        public void Load()
        {
            if(!ES3.KeyExists($"{name} ClickDamage")) return;
            if(!ES3.KeyExists($"{name} ClickHeal")) return;
            
            RestoreLevels();
            SetStats(ES3.Load<Stat>($"{name} ClickDamage"),ES3.Load<Stat>($"{name} ClickHeal") );
        }
        
        public void Restore()
        {
            RestoreLevels();
        }
    }
}