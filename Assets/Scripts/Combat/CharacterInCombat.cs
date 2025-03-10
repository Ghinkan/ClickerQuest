using System.Collections.Generic;
using ClickerQuest.Characters;
using ClickerQuest.Characters.Buffs;
using ClickerQuest.Characters.Player;
using ClickerQuest.UI.Combat;
using Timers;
using UnityEngine;
using UnityEngine.Events;
namespace ClickerQuest.Combat
{
    public class CharacterInCombat : MonoBehaviour
    {
        public event UnityAction CharacterAdded;
        public event UnityAction BuffsChanged;
        public event UnityAction Attacked;
        public event UnityAction SkillReady;
        public event UnityAction SkillUsed;
        public event UnityAction Died;
        
        [field: SerializeField] public Character Character { get; private set; }
        public CountdownTimer AttackTimer;
        public CountdownTimer SkillTimer;
        
        [SerializeField] private Transform _visualTransform;
        [SerializeField] private Collider2D _collider2D;

        
        //TODO: Decouple PopUpSpawner
        [SerializeField] private PopUpSpawner _popUpSpawner;
        
        [field: SerializeField] public List<Buff> Buffs { get; private set; }
        
        //TODO: Decouple AnimationController
        private CharacterAnimationController _characterAnimationController;
        
        
        public void Initialize(Character character)
        {
            Character = character;
            Character.Health.Died += CharacterDie;
            
            _characterAnimationController = Instantiate(Character.VisualPrefab, _visualTransform).GetComponent<CharacterAnimationController>();
            _characterAnimationController.Initialize(this);
            _characterAnimationController.AttackHits += AttackHits;
            _characterAnimationController.SkillHits += SkillEffect;
            
            _popUpSpawner.Initialize(character);
            
            AttackTimer = new CountdownTimer(Character.Stats.AttackRate.ActualValue, this);
            AttackTimer.OnTimerStop += Attack;
            AttackTimer.Start();
            
            SkillTimer = new CountdownTimer(Character.Stats.SkillRate.ActualValue, this);
            SkillTimer.OnTimerStop += SkillIsReady;
            SkillTimer.Start();
            
            CharacterAdded?.Invoke();
        }
        
        private void OnDisable()
        {
            Character.Health.Died -= CharacterDie;
            
            _characterAnimationController.AttackHits -= AttackHits;
            _characterAnimationController.SkillHits -= SkillEffect;
            
            AttackTimer.OnTimerStop -= Attack;
            SkillTimer.OnTimerStop -= SkillIsReady;
        }

        public void Attack()
        {
            Attacked?.Invoke();
        }
        
        private void AttackHits()
        {
            AttackTimer.Restart();

            Character objective = Character.SelectRandomObjective();
            if(objective)
                objective.Health.Decrement(Character.Stats.Attack.ActualValue);
        }
        
        private void SkillIsReady()
        {
            if (Character is Enemy)
                if (Character.Skill.IsInstant)
                    SkillEffect();
                else
                    UseSkill();
            else
                SkillReady?.Invoke();
        }
        
        public void UseSkill()
        {
            SkillUsed?.Invoke();
        }
        
        private void SkillEffect()
        {
            SkillTimer.Restart();
            AttackTimer.Restart();
            Character.Skill.Effect(this);
        }
        
        private void CharacterDie()
        {
            _collider2D.enabled = false;
            
            Character.CharacterDefeated.RaiseEvent(Character);
            
            SkillTimer.Dispose();
            AttackTimer.Dispose();
            
            Died?.Invoke();
        }
        
        public void Despawn()
        {
            Character.CharacterDespawn.RaiseEvent(gameObject);
            Destroy(gameObject);
        }

        public void EndCombat()
        {
            _collider2D.enabled = false;
            AttackTimer.Pause();
            SkillTimer.Pause();
        }

        public void Clicked(ClickerStats clickerStats)
        {
            foreach (Buff buff in new List<Buff>(Buffs))
                buff.CharacterClicked();
            
            Character.Clicked(clickerStats);
        }
        
        public void AddBuff(Buff buff)
        {
            if (!Buffs.Contains(buff))
                Buffs.Add(buff);
            else
                Buffs[Buffs.IndexOf(buff)] = Instantiate(buff);
            
            BuffsChanged?.Invoke();
        }
        
        public void RemoveBuff(Buff buff)
        {
            if (Buffs.Contains(buff))
            {
                Buffs.Remove(buff);
                BuffsChanged?.Invoke();
            }
        }
    }
}