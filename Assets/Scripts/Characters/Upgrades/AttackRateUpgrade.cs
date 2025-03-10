using UnityEngine;
namespace ClickerQuest.Characters.Upgrades
{
    [CreateAssetMenu(fileName = "AttackRateUpgrade", menuName = "Upgrade/AttackRateUpgrade")]
    public class AttackRateUpgrade : Upgrade
    {
        public override void Apply(Character character)
        {
            character.Stats.AttackRate.AddModifier(Modifier);
            character.LevelUp();
        }
    }
}