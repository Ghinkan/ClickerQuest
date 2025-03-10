using UnityEngine;
using UnityEngine.UI;
namespace SceneManagement
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(ExitGame);
        }
        
        public void ExitGame()
        {
#if !UNITY_WEBGL   
            Application.Quit();
#endif
        }
    }
}