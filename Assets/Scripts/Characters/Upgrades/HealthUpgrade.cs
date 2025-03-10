using UnityEngine;
namespace ClickerQuest.Characters.Upgrades
{    
    [CreateAssetMenu(fileName = "HealthUpgrade", menuName = "Upgrade/HealthUpgrade")]
    public class HealthUpgrade : Upgrade
    {
        public override void Apply(Character character)
        {
            character.Stats.MaxHealth.AddModifier(Modifier);
            character.LevelUp();
            character.Health.Restore();
        }
    }
}