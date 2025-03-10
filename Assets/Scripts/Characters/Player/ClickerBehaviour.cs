using UnityEngine;
namespace ClickerQuest.Characters.Player
{
    public abstract class ClickerBehaviour: ScriptableObject
    {
        [SerializeField] protected ClickerStats ClickerStats;
        public bool PowerUpActive { get; protected set; }
        
        protected Camera Camera; 
        
        public abstract void Initialize();
        public abstract void ClickBehaviour();
        public abstract void ActivatePowerUp();
    }
}