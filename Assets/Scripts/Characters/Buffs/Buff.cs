using ClickerQuest.Combat;
using Timers;
using UnityEngine;
namespace ClickerQuest.Characters.Buffs
{
    public abstract class Buff : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int Duration { get; private set; }

        protected CharacterInCombat CharacterInCombat;
        protected CountdownTimer Timer;

        public void AddBuff(CharacterInCombat characterInCombat)
        {
            StartBuff(characterInCombat);
        } 

        protected abstract void StartBuff(CharacterInCombat characterInCombat);

        public virtual void StartAttack() { }
        
        public virtual void CharacterClicked() { }
        
        protected abstract void Effect();

        public virtual void EndBuff(CharacterInCombat characterInCombat)
        {
            Timer.Dispose();
            characterInCombat.RemoveBuff(this);
        }
    }
}