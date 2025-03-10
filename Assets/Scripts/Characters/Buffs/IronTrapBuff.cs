using ClickerQuest.Combat;
using Timers;
using UnityEngine;
namespace ClickerQuest.Characters.Buffs
{
    [CreateAssetMenu(fileName = "IronTrapBuff", menuName = "Buff/IronTrapBuff")]
    public class IronTrapBuff : Buff
    {
        protected override void StartBuff(CharacterInCombat characterInCombat)
        {
            CharacterInCombat = characterInCombat;
            
            IronTrapBuff ironTrapBuff = Instantiate(this);
            ironTrapBuff.CharacterInCombat = CharacterInCombat;
            
            ironTrapBuff.Timer = new CountdownTimer(Duration);
            ironTrapBuff.Timer.OnTimerStop += ironTrapBuff.EndBuff;
            ironTrapBuff.Timer.Start();
            
            ironTrapBuff.Effect();
            
            CharacterInCombat.AddBuff(ironTrapBuff);
        }
        
        private void EndBuff()
        {
            CharacterInCombat.AttackTimer.Resume();
            CharacterInCombat.SkillTimer.Resume();
            Timer.OnTimerStop -= EndBuff;
            EndBuff(CharacterInCombat);
        }
        
        protected override void Effect()
        {
            CharacterInCombat.AttackTimer.Pause();
            CharacterInCombat.SkillTimer.Pause();
        }
    }
}