using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Common.Scenes
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _runner;

        public SceneLoader(ICoroutineRunner runner) => 
            _runner = runner;

        public void LoadScene(string sceneName, Action onLoaded = null) => 
            _runner.StartCoroutine(Loading(sceneName, onLoaded));

        private static IEnumerator Loading(string nextSceneName, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == nextSceneName)
            {
                onLoaded?.Invoke();
                yield break;
            }

            var waitNextScene = SceneManager.LoadSceneAsync(nextSceneName);

            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}