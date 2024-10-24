using CodeBase.Infrastructure.Common;
using CodeBase.Infrastructure.Common.AssetManagement;
using CodeBase.Infrastructure.Common.Localization;
using CodeBase.Infrastructure.Common.Scenes;
using CodeBase.Infrastructure.Installers.Extensions;
using Reflex.Core;
using UnityEngine;

namespace CodeBase.Infrastructure.Installers.Bindings
{
    public static class InfrastructureBindingExtensions
    {
        public static ContainerBuilder BindInfrastructure(this ContainerBuilder builder)
        {
            //BindCoroutineRunner(builder);
            BindSceneLoading(builder);
            BindAssetManagement(builder);
            BindLocalization(builder);
            return builder;
        }

        private static void BindCoroutineRunner(ContainerBuilder builder)
        {
            builder.AddSingleton((container) =>
            {
                var newGameObject = new GameObject(nameof(CoroutineRunner));
                var coroutineRunner = newGameObject.AddComponent<CoroutineRunner>();
                Object.DontDestroyOnLoad(newGameObject);
                return coroutineRunner;
            }, typeof(ICoroutineRunner));
        }

        private static void BindSceneLoading(ContainerBuilder builder)
        {
            builder.AddSingleton<SceneLoader>();
        }

        private static void BindAssetManagement(ContainerBuilder builder)
        {
            builder.AddSingleton<AssetProvider>();
            builder.AddSingleton<AssetDownloadReporter>();
            builder.AddSingleton<AssetDownloadService>();

            builder.AddSingleton<LevelDownloadService>();
        }

        private static void BindLocalization(ContainerBuilder builder)
        {
            builder.AddSingleton<LocalizationService>();
        }
    }
}