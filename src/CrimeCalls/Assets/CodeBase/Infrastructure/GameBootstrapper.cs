using CodeBase.Infrastructure.Common.Scenes;
using CodeBase.Infrastructure.Common.Scenes.UI;
using Reflex.Attributes;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        private LoadingCurtain _curtain;

        [Inject]
        public void Construct(SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        private void Start()
        {
            _curtain.Show();
            
            _sceneLoader.LoadScene(SceneNames.GameLoop, () =>
            {
                _curtain.Hide();
            });
        }
    }
}