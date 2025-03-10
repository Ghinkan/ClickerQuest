using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.UI;
namespace SceneManagement
{
    public class ChangeSceneButton : MonoBehaviour
    {
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private Button _button;
        [SerializeField] private SceneReference _scene;
        [SerializeField] private bool _withLoadScene;
        
        private void Start()
        {
            if (_withLoadScene)
                _button.onClick.AddListener(() => _sceneController.LoadSceneWithLoadScene(_scene));
            else
                _button.onClick.AddListener(() => _sceneController.LoadScene(_scene));
        }
    }
}