using CodeBase.Infrastructure.Common.AssetManagement;
using CodeBase.Infrastructure.Common.Localization;
using CodeBase.Infrastructure.Installers.Extensions;
using Reflex.Core;

namespace CodeBase.Infrastructure.Installers.Bindings
{
    public static class InfrastructureBindingExtensions
    {
        public static ContainerBuilder BindInfrastructure(this ContainerBuilder builder)
        {
            BindAssetManagement(builder);
            BindLocalization(builder);
            return builder;
        }

        private static void BindAssetManagement(ContainerBuilder builder)
        {
            builder.AddSingleton<AssetProvider>();
            builder.AddSingleton<AssetDownloadReporter>();
            builder.AddSingleton<AssetDownloadService>();
        }

        private static void BindLocalization(ContainerBuilder builder)
        {
            builder.AddSingleton<LocalizationService>();
        }
    }
}