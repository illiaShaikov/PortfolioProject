using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string loadScene, Action OnLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(loadScene, OnLoaded));
        }

        private IEnumerator LoadScene(string loadScene, Action OnLoaded = null)
        {
            if(SceneManager.GetActiveScene().name == loadScene)
            {
                OnLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(loadScene);

            while(!waitNextScene.isDone)
            {
                yield return null;
            }  

            OnLoaded?.Invoke();
        }
    }
}