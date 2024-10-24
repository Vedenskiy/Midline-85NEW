using CodeBase.Infrastructure.Common;
using CodeBase.Infrastructure.Common.Scenes.UI;
using Reflex.Core;
using UnityEngine;

namespace CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private LoadingCurtain _loadingCurtain;

        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(_coroutineRunner, typeof(ICoroutineRunner));
            builder.AddSingleton(_loadingCurtain, typeof(LoadingCurtain));
        }
    }
}