using CodeBase.Infrastructure.Common.AssetManagement;
using CodeBase.Infrastructure.Common.Localization;
using CodeBase.Infrastructure.Common.Scenes;
using CodeBase.Infrastructure.Installers.Extensions;
using Reflex.Core;

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

        private static void BindSceneLoading(ContainerBuilder builder)
        {
            builder.AddSingleton<SceneLoader>();
        }

        private static void BindAssetManagement(ContainerBuilder builder)
        {
            builder.AddSingleton<AssetProvider>();
            builder.AddSingleton<AssetDownloadReporterRegistry>();

            builder.AddSingleton<LevelDownloadService>();
        }

        private static void BindLocalization(ContainerBuilder builder)
        {
            builder.AddSingleton<LocalizationService>();
        }
    }
}