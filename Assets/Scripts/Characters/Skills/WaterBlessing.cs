using System.Collections.Generic;
using ClickerQuest.Combat;
using UnityEngine;
namespace ClickerQuest.Characters.Skills
{
    [CreateAssetMenu(fileName = "WaterBlessing", menuName = "Skill/WaterBlessing")]
    public class WaterBlessing : Skill
    {
        [SerializeField] private CharactersInBattle _charactersInBattle;
        [SerializeField] private int _healAmount = 50;

        public override void Effect(CharacterInCombat characterInCombat)
        {
            foreach (CharacterInCombat ally in new List<CharacterInCombat>(_charactersInBattle.Heroes))
            {
                ally.Character.Health.Increment(_healAmount);
            }
            
            Debug.Log("WaterBlessing Effect");
        }
    }
}