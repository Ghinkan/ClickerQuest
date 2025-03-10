using ClickerQuest.Characters.Player;
using UnityEngine;
namespace ClickerQuest.Characters.Upgrades
{
    [CreateAssetMenu(fileName = "AttackClickUpgrade", menuName = "Upgrade/AttackClickUpgrade")]
    public class AttackClickUpgrade : Upgrade
    {
        [SerializeField] private ClickerStats _clickerStats;

        public override void Apply(Character character)
        {
            _clickerStats.ClickDamage.AddModifier(Modifier);
            _clickerStats.LevelUp();

        }
    }
}