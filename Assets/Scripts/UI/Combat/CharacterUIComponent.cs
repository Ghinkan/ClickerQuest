using UnityEngine;
namespace ClickerQuest.UI.Combat
{
    public abstract class CharacterUIComponent : MonoBehaviour
    {
        protected CharacterUI CharacterUI{get; private set;}

        protected virtual void OnEnable()
        {
            CharacterUI = transform.parent.GetComponent<CharacterUI>();
            CharacterUI.CharacterInCombat.CharacterAdded += Initialize;
        }
        
        private void OnDisable()
        {
            CharacterUI.CharacterInCombat.CharacterAdded -= Initialize;
        }

        protected abstract void Initialize();
    }
}