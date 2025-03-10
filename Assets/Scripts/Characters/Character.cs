using AudioSystem;
using ClickerQuest.Characters.Player;
using ClickerQuest.Characters.Skills;
using ClickerQuest.Combat;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventChannels;
using UnityEngine.Events;
namespace ClickerQuest.Characters
{
    public abstract class Character : ScriptableObject
    {
        public event UnityAction CharacterLevelUp;
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Portrait { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public CharacterAnimationController VisualPrefab { get; private set; }
        [field: SerializeField] public Skill Skill { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public Stats Stats { get; private set; }
        [field:SerializeField] public CharacterEventChannel CharacterDefeated { get; private set; }
        [field:SerializeField] public GameObjectEventChannel CharacterDespawn { get; private set; }
        [field: SerializeField] public SoundData AttackSound { get; private set; }
        [field: SerializeField] public SoundData DeathSound { get; private set; }
        
        [SerializeField] protected CharactersInBattle CharactersInBattle;
        
        public void SetStats(Stats stats)
        {
            Stats = stats;
            Health = new Health(Stats.MaxHealth.ActualValue);
        }

        public void LevelUp()
        {
            Level++;
            CharacterLevelUp?.Invoke();
        }

        [Button]
        public void RestoreLevels()
        {
            Level = 1;
            Stats.MaxHealth.RemoveAllModifiers();
            Stats.Attack.RemoveAllModifiers();
            Stats.AttackRate.RemoveAllModifiers();
            Stats.SkillRate.RemoveAllModifiers();
        }

        public abstract    Character SelectRandomObjective();
        public abstract    void      Clicked(ClickerStats clickerStats);
    }
}