using AudioSystem;
using ClickerQuest.Characters.Player;
using UnityEngine;
using UnityEngine.Utils;
namespace ClickerQuest.Characters
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Characters/Enemy")]
    public class Enemy : Character
    {
        [field:SerializeField] public int Gold { get; private set; }
        
        public override Character SelectRandomObjective()
        {
            if (CharactersInBattle.Enemies.Count <= 0)
                return null;
            
            Character heroAttacked = CharactersInBattle.Heroes.RandomItem().Character;
            Debug.Log($"{name} Attack {heroAttacked.name}");
            return heroAttacked; 
        }
        
        public override void Clicked(ClickerStats clickerStats)
        {
            Health.Decrement(clickerStats.ClickDamage.ActualValue);
            SoundManager.Instance.CreateSoundBuilder().Play(Health.IsInvulnerable ? clickerStats.BlockedDamageSound : clickerStats.DamageSound);
        }
    }
}