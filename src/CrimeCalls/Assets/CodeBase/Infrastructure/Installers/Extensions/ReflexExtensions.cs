using Reflex.Core;

namespace CodeBase.Infrastructure.Installers.Extensions
{
    public static class ReflexExtensions
    {
        public static ContainerBuilder AddSingleton<TService>(this ContainerBuilder builder) => 
            builder.AddSingleton(typeof(TService));
    }
}