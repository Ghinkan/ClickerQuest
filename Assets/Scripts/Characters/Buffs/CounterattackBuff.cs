using ClickerQuest.Combat;
using Timers;
using UnityEngine;
namespace ClickerQuest.Characters.Buffs
{
    [CreateAssetMenu(fileName = "CounterattackBuff", menuName = "Buff/CounterattackBuff")]
    public class CounterattackBuff : Buff
    {
        protected override void StartBuff(CharacterInCombat characterInCombat)
        {
            CharacterInCombat = characterInCombat;
            
            CounterattackBuff counterattackBuff = Instantiate(this);
            counterattackBuff.CharacterInCombat = CharacterInCombat;
            
            counterattackBuff.Timer = new CountdownTimer(Duration);
            counterattackBuff.Timer.OnTimerStop += counterattackBuff.EndBuff;
            counterattackBuff.Timer.Start();
            
            CharacterInCombat.SkillTimer.Pause();
            
            CharacterInCombat.AddBuff(counterattackBuff);
        }
        
        private void EndBuff()
        {
            Timer.OnTimerStop -= EndBuff;
            CharacterInCombat.SkillTimer.Resume();
            EndBuff(CharacterInCombat);
        }

        public override void CharacterClicked()
        {
            Effect();
        }
        
        protected override void Effect()
        {
            CharacterInCombat.UseSkill();
            EndBuff();
        }
    }
}