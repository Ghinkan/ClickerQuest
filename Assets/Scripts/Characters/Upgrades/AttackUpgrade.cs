using UnityEngine;
namespace ClickerQuest.Characters.Upgrades
{
    [CreateAssetMenu(fileName = "AttackUpgrade", menuName = "Upgrade/AttackUpgrade")]
    public class AttackUpgrade : Upgrade
    {
        public override void Apply(Character character)
        {
            character.Stats.Attack.AddModifier(Modifier);
            character.LevelUp();
        }
    }
}