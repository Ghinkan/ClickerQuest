using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Stats
{
    [System.Serializable]
    [HideReferenceObjectPicker]
    public class Stat
    {
        private bool _haveToRecalculate = true;
        private int _actualValue;
        
        [field: SerializeField] public int BaseValue { get; private set; }
        [ShowInInspector] public int ActualValue
        {
            get 
            {
                if(_haveToRecalculate) 
                {
                    _actualValue = CalculateFinalValue();
                    _haveToRecalculate = false;
                }
                return _actualValue;
            }
            set => _actualValue = value;
        }
        
        [field: SerializeField] public List<StatModifier> StatModifiers { get; private set; }
        
        [Button]
        public void Recalculate() => _haveToRecalculate = true;
        
        public Stat()
        {
            StatModifiers = new List<StatModifier>();
        }

        public Stat(int minValue, int maxValue)
        {
            BaseValue = Random.Range(minValue, maxValue);
            ActualValue = BaseValue;
            StatModifiers = new List<StatModifier>();
        }
        
        public Stat(Stat stat)
        {
            BaseValue = stat.BaseValue;
            StatModifiers = new List<StatModifier>(stat.StatModifiers);
            ActualValue = stat.ActualValue;
        }

        public void AddModifier(StatModifier modifier)
        {
            _haveToRecalculate = true;
            StatModifiers.Add(modifier);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _haveToRecalculate = true;
            StatModifiers.Remove(modifier);
        }
        
        public void RemoveAllModifiersFromSource(Object source)
        {
            _haveToRecalculate = true;
            for (int i = StatModifiers.Count - 1; i >= 0; i--)
                if (StatModifiers[i].Source == source)
                    StatModifiers.RemoveAt(i);
        }
        
        public void RemoveAllModifiers()
        {
            _haveToRecalculate = true;
            StatModifiers.Clear();
        }
        
        private int CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;
            float sumPercentMultiplier = 0;

            StatModifiers.Sort(CompareModifierOrder);
            for (int i = 0; i < StatModifiers.Count; i++)
            {
                StatModifier mod = StatModifiers[i];

                switch (mod.Type)
                {
                    case StatModType.Flat:
                        finalValue += mod.Value;
                        break;
                    
                    case StatModType.PercentAdditive:
                    {
                        sumPercentAdd += mod.Value;

                        if (IsLastModifier(i) || IsNextModifierNotPercentAdditive(i))
                        {
                            finalValue *= 1 + sumPercentAdd;
                            sumPercentAdd = 0;
                        }
                        break;
                    }
                    case StatModType.PercentMultiplicative:
                    {
                        sumPercentMultiplier += mod.Value;
                       
                        if (IsLastModifier(i))
                        {
                            finalValue *= 1 + sumPercentMultiplier;
                            sumPercentMultiplier = 0;
                        }
                        break;
                    }
                }
            }
            
            return Mathf.RoundToInt(Mathf.Max(finalValue, 0));
        }
        
        private bool IsLastModifier(int currentIndex) => currentIndex + 1 >= StatModifiers.Count;

        private bool IsNextModifierNotPercentAdditive(int index) => StatModifiers[index + 1].Type != StatModType.PercentAdditive;

        private int  CompareModifierOrder(StatModifier a, StatModifier b) => a.Type.CompareTo(b.Type);
    }
}
