using ClickerQuest.Combat;
using Timers;
using UnityEngine;
namespace ClickerQuest.Characters.Buffs
{
    [CreateAssetMenu(fileName = "ShieldBuff", menuName = "Buff/ShieldBuff")]
    public class ShieldBuff : Buff
    {
        protected override void StartBuff(CharacterInCombat characterInCombat)
        {
            CharacterInCombat = characterInCombat;
            
            ShieldBuff shieldBuff = Instantiate(this);
            shieldBuff.CharacterInCombat = CharacterInCombat;
            
            shieldBuff.Timer = new CountdownTimer(Duration);
            shieldBuff.Timer.OnTimerStop += shieldBuff.EndBuff;
            shieldBuff.Timer.Start();
            
            shieldBuff.Effect();
            
            CharacterInCombat.AddBuff(shieldBuff);
        }
        
        private void EndBuff()
        {
            CharacterInCombat.Character.Health.IsInvulnerable = false;
            CharacterInCombat.AttackTimer.Resume();
            CharacterInCombat.SkillTimer.Resume();
            Timer.OnTimerStop -= EndBuff;
            EndBuff(CharacterInCombat);
        }
        
        protected override void Effect()
        {
            CharacterInCombat.Character.Health.IsInvulnerable = true;
            CharacterInCombat.UseSkill();
            CharacterInCombat.AttackTimer.Pause();
            CharacterInCombat.SkillTimer.Pause();
        }
    }
}