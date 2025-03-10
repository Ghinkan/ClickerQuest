using AudioSystem;
using ClickerQuest.Combat;
using UnityEngine;
namespace ClickerQuest.Characters.Skills
{

    public abstract class Skill : ScriptableObject
    {
        [field:SerializeField] public Sprite SkillIcon { get; private set; }
        [field:SerializeField] public SoundData SoundFX { get; private set; }
        [field:SerializeField] public bool IsInstant { get; private set; }

        public abstract void Effect(CharacterInCombat characterInCombat);
    }
}