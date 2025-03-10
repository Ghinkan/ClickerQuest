using ClickerQuest.Characters.Player;
using UnityEngine;
namespace ClickerQuest.Characters.Upgrades
{
    [CreateAssetMenu(fileName = "HealClickUpgrade", menuName = "Upgrade/HealClickUpgrade")]
    public class HealClickUpgrade : Upgrade
    {
        [SerializeField] private ClickerStats _clickerStats;
        public override void Apply(Character character)
        {
            _clickerStats.ClickHeal.AddModifier(Modifier);
            _clickerStats.LevelUp();
        }
    }
}