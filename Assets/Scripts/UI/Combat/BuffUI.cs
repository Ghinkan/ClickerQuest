using ClickerQuest.Characters.Buffs;
using UnityEngine;
using UnityEngine.UI;
namespace ClickerQuest.UI.Combat
{
    public class BuffUI : MonoBehaviour
    {
        [SerializeField] private Image _buffSprite;
        
        public void Initialize(Buff buff)
        {
            _buffSprite.sprite = buff.Sprite;
        }
    }
}