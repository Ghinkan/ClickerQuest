using ClickerQuest.Characters.Buffs;
using ClickerQuest.Combat;
using UnityEngine;
namespace ClickerQuest.Characters.Skills
{
    [CreateAssetMenu(fileName = "BoneShield", menuName = "Skill/BoneShield")]
    public class BoneShield : Skill
    {
        [SerializeField] private Buff _shieldBuff;
        
        public override void Effect(CharacterInCombat characterInCombat)
        {
            _shieldBuff.AddBuff(characterInCombat);
        }
    }
}