using Sirenix.OdinInspector;
using UnityEngine;
namespace Stats
{
    [System.Serializable]
    [InlineProperty]
    [HideLabel]
    public class StatModifier
    {
        public Object Source;
        public StatModType Type;
        public float Value;

        public StatModifier(float value, StatModType type, Object source)
        {
            Value = value;
            Type = type;
            Source = source;
        }
        public StatModifier(StatModifier statModifier)
        {
            Value = statModifier.Value;
            Type = statModifier.Type;
            Source = statModifier.Source;
        }
    }
}