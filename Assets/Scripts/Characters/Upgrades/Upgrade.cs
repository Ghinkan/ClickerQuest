using Stats;
using UnityEngine;
namespace ClickerQuest.Characters.Upgrades
{
    public abstract class Upgrade : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public StatModifier Modifier { get; private set; }

        public abstract void Apply(Character character);
    }
}