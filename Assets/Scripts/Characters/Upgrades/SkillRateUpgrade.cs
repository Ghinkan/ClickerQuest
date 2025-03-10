using UnityEngine;
namespace ClickerQuest.Characters.Upgrades
{
    [CreateAssetMenu(fileName = "SkillRateUpgrade", menuName = "Upgrade/SkillRateUpgrade")]
    public class  SkillRateUpgrade : Upgrade
    {
        public override void Apply(Character character)
        {
            character.Stats.SkillRate.AddModifier(Modifier);
            character.LevelUp();
        }
    }
}