using PoolSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
namespace ClickerQuest.UI.Combat
{
    public class PopUpNumber : MonoBehaviour ,IPoolable<PopUpNumber>
    {
        [SerializeField] private TMP_Text _text;
        
        private UnityAction<PopUpNumber> _returnAction;

        public void Setup(int amount, Color color)
        {
            _text.text = amount.ToString();
            _text.color = color;
        }

        //AnimationEvent
        public void DestroySelf()
        {
            ReturnToPool();
        }
        
        public void Initialize(UnityAction<PopUpNumber> returnAction) => _returnAction = returnAction;
        public void ReturnToPool()                                    => _returnAction?.Invoke(this);
    }
}