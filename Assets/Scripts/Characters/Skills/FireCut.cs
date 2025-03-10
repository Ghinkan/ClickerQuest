using System.Collections.Generic;
using ClickerQuest.Combat;
using UnityEngine;
namespace ClickerQuest.Characters.Skills
{
    [CreateAssetMenu(fileName = "FireCut", menuName = "Skill/FireCut")]
    public class FireCut : Skill
    {
        [SerializeField] private CharactersInBattle _charactersInBattle;
        [SerializeField] private int _multiplier; 
        
        public override void Effect(CharacterInCombat characterInCombat)
        {
            foreach (CharacterInCombat enemy in new List<CharacterInCombat>(_charactersInBattle.Enemies))
                enemy.Character.Health.Decrement(characterInCombat.Character.Stats.Attack.ActualValue * _multiplier);
        }
    }

}