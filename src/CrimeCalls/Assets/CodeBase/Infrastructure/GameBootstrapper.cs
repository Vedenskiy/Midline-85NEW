using System;
using CodeBase.Infrastructure.Common.Scenes;
using Reflex.Attributes;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        [Inject]
        public void Construct(SceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        private void Start()
        {
            _sceneLoader.LoadScene(SceneNames.GameLoop, () => {Debug.Log("LOADED!");});
        }
    }
}