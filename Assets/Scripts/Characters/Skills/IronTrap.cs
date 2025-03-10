using ClickerQuest.Characters.Buffs;
using ClickerQuest.Combat;
using UnityEngine;
using UnityEngine.Utils;
namespace ClickerQuest.Characters.Skills
{
    [CreateAssetMenu(fileName = "IronTrap", menuName = "Skill/IronTrap")]
    public class IronTrap : Skill
    {
        [SerializeField] private CharactersInBattle _charactersInBattle;
        [SerializeField] private Buff _ironTrapBuff;
        
        public override void Effect(CharacterInCombat characterInCombat)
        {
            _ironTrapBuff.AddBuff(_charactersInBattle.Enemies.RandomItem());
        }
    }
}