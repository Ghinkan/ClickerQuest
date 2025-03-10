using ClickerQuest.Combat;
using UnityEngine;
namespace ClickerQuest.Characters.Skills
{
    [CreateAssetMenu(fileName = "MushroomBite", menuName = "Skill/MushroomBite")]
    public class MushroomBite : Skill
    {
        public override void Effect(CharacterInCombat characterInCombat)
        { 
           characterInCombat.Attack();
           characterInCombat.Character.Health.Increment(characterInCombat.Character.Stats.Attack.ActualValue);
        }
    }
}