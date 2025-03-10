using Sirenix.OdinInspector;
using Stats;
using UnityEngine;
namespace ClickerQuest.Characters
{
    [System.Serializable]    
    [InlineProperty]
    [HideLabel]
    public struct Stats
    {
        [field:SerializeField]
        [field:InlineProperty]
        [field:HideLabel]
        [field:BoxGroup(nameof(MaxHealth), centerLabel: true)] 
        public Stat MaxHealth{ get; private set; }
        
        [field:SerializeField]
        [field:InlineProperty]
        [field:HideLabel]
        [field:BoxGroup(nameof(Attack), centerLabel: true)]
        public Stat Attack{ get; private set; }
        
        [field:SerializeField]
        [field:InlineProperty]
        [field:HideLabel]
        [field:BoxGroup(nameof(AttackRate), centerLabel: true)]
        public Stat AttackRate{ get; private set; }
        
        [field:SerializeField]
        [field:InlineProperty]
        [field:HideLabel]
        [field:BoxGroup(nameof(SkillRate), centerLabel: true)]
        public Stat SkillRate{ get; private set; }
    }
}