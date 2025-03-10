using PoolSystem;
using UnityEngine;
using UnityEngine.Utils;
namespace ClickerQuest.UI.Combat
{
    public class PopUpPool : Singleton<PopUpPool>
    {
        [field: SerializeField] public PopUpNumber Object { get; private set; }
        public ObjectPool<PopUpNumber> Pool{ get; private set; }
        
        private void OnEnable() => Pool = new ObjectPool<PopUpNumber>(Object, transform);
    }
}