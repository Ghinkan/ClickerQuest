using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
namespace ClickerQuest.Characters
{
    [System.Serializable]
    [InlineProperty]
    [HideLabel]
    public class Health
    {
        public event UnityAction<float> HealthChangedPercentage;
        public event UnityAction<int> HealthChanged;
        public event UnityAction Died;
        
        [ShowInInspector] public int CurrentHealth{ get; private set; }
        public bool IsInvulnerable;
        
        private int _maxHealth;

        public Health(int maxHealth)
        {
            SetMaxHealth(maxHealth);
        }
        
        public void SetMaxHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            Restore();
        }

        public float HealthPercentage()
        {
            return (float)CurrentHealth / _maxHealth;
        }

        public void Increment(int amount)
        {
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
            HealthChanged?.Invoke(amount);
            UpdateHealth();
        }

        public void Decrement(int amount)
        {
            if (IsInvulnerable)
                amount = 0;
            
            CurrentHealth -= amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
            HealthChanged?.Invoke(-amount);
            UpdateHealth();
            
            if (CurrentHealth <= 0)
                Died?.Invoke();
        }
        
        public void Restore()
        {
            CurrentHealth = _maxHealth;
            UpdateHealth();
        }
        
        public void UpdateHealth()
        {
            HealthChangedPercentage?.Invoke(HealthPercentage());
        }
    }
}