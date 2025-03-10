using AudioSystem;
using ClickerQuest.Combat;
using UnityEngine;
using UnityEngine.Events;
namespace ClickerQuest.Characters
{
    public class CharacterAnimationController : MonoBehaviour
    {
        public event UnityAction AttackHits;
        public event UnityAction SkillHits;
        
        [SerializeField] private Animator _animator;

        private CharacterInCombat _characterInCombat;
        private SoundBuilder _soundBuilder;
        
        public void Initialize(CharacterInCombat characterInCombat)
        {
            _characterInCombat = characterInCombat;
            _characterInCombat.Attacked += PlayAttack;
            _characterInCombat.SkillUsed += PlaySkill;
            _characterInCombat.Died += PlayDeath;
            
            _soundBuilder = SoundManager.Instance.CreateSoundBuilder().WithPosition(transform.position);
        }
        
        private void OnDisable()
        {
            _characterInCombat.Attacked -= PlayAttack;
            _characterInCombat.SkillUsed -= PlaySkill;
            _characterInCombat.Died -= PlayDeath;
        }

        private void PlayAttack()
        {
            _animator.SetTrigger("Attack");
        }
        
        private void PlaySkill()
        {
            _animator.SetTrigger("Skill");
        }

        private void PlayDeath()
        {
            _animator.SetTrigger("Die");
            _soundBuilder.Play(_characterInCombat.Character.DeathSound);
        }

        private void AttackHit()
        {
            AttackHits?.Invoke();
            _soundBuilder.Play(_characterInCombat.Character.AttackSound);
        }
        
        private void SkillHit()
        {
            SkillHits?.Invoke();
            _soundBuilder.Play(_characterInCombat.Character.Skill.SoundFX);
        }

        private void Despawm()
        {
            _characterInCombat.Despawn();
        }
    }
}