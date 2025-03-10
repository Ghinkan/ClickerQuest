using System.Collections;
using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace SceneManagement
{
    [CreateAssetMenu(fileName = "SceneController", menuName = "Controllers/SceneController")]
    public class SceneController : ScriptableObject
    {
        private UnityAction<MonoBehaviour> _onLoaderCallback;
        private AsyncOperation _loadingAsyncOperation;
        [SerializeField] private SceneReference _loadingScene;
        
        public void LoaderCallback(MonoBehaviour callback)
        {
            _onLoaderCallback?.Invoke(callback);
            _onLoaderCallback = null;
        }	
        
        public float GetLoadingProgress()
        {
            if (_loadingAsyncOperation != null)
                return _loadingAsyncOperation.progress;
			
            return 0f;
        }
        
        public void LoadScene(SceneReference scene)
        {
            _onLoaderCallback = null;
            _loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.Name);
        }
        
        public void LoadSceneWithLoadScene(SceneReference scene)
        {
            _onLoaderCallback = callback => { callback.StartCoroutine(LoadSceneAsync(scene)); };

            SceneManager.LoadScene(_loadingScene.Name);
        }
        
        private IEnumerator LoadSceneAsync(SceneReference scene)
        {
            yield return null;
            _loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.Name);

            while (!_loadingAsyncOperation.isDone)
                yield return null;
        }
        
        public void ExitGame() => Application.Quit();
    }
}