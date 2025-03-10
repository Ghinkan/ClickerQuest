using ClickerQuest.Characters.Buffs;
using ClickerQuest.Combat;
using UnityEngine;
namespace ClickerQuest.Characters.Skills
{
    [CreateAssetMenu(fileName = "GoblinCounterattack", menuName = "Skill/GoblinCounterattack")]
    public class GoblinCounterattack : Skill
    {
        [SerializeField] private Buff _counterattackBuff;
        public override void Effect(CharacterInCombat characterInCombat)
        {
            _counterattackBuff.AddBuff(characterInCombat);
        }
    }
}