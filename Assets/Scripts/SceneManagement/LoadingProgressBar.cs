using UnityEngine;
using UnityEngine.UI;
namespace SceneManagement
{
	public class LoadingProgressBar : MonoBehaviour
	{
		[SerializeField] private SceneController _sceneController;
		[SerializeField] private Image _image;
		
		private void Start()  => _sceneController.LoaderCallback(this);
		private void Update() => _image.fillAmount = _sceneController.GetLoadingProgress();
	}
}