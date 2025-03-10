using Stats;
using UnityEngine;
namespace ClickerQuest.Characters
{
    [CreateAssetMenu(fileName = "EnemyLevelUpStats", menuName = "EnemyLevelUpStats", order = 0)]
    public class EnemyLevelUpStats : ScriptableObject
    {
        [SerializeField] private StatModifier _maxHealthPerLevel;
        [SerializeField] private StatModifier _attackPerLevel;
        [SerializeField] private StatModifier _attackRatePerLevel;
        [SerializeField] private StatModifier _skillRatePerLevel;
        
        public void LevelUp(Character character)
        {
            character.Stats.MaxHealth.AddModifier(new StatModifier(_maxHealthPerLevel));
            character.Stats.Attack.AddModifier(new StatModifier(_attackPerLevel));
            character.Stats.AttackRate.AddModifier(new StatModifier(_attackRatePerLevel));
            character.Stats.SkillRate.AddModifier(new StatModifier(_skillRatePerLevel));
            character.LevelUp();
        }
    }
}