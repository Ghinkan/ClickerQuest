using ClickerQuest.Combat;
using UnityEngine;
namespace ClickerQuest.UI.Combat
{
    public class CharacterUI : MonoBehaviour
    {
        [field: SerializeField] public CharacterInCombat CharacterInCombat {get; private set;}

        private void OnEnable()
        {
            CharacterInCombat.Died += CharacterInCombatOnDied;
        }

        private void OnDisable()
        {
            CharacterInCombat.Died -= CharacterInCombatOnDied;
        }
        
        private void CharacterInCombatOnDied()
        {
            gameObject.SetActive(false);
        }
    }
}