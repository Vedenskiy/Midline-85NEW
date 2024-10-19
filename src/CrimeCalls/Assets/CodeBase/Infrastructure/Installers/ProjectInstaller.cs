using CodeBase.Infrastructure.Installers.Bindings;
using Reflex.Core;
using UnityEngine;

namespace CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.BindInfrastructure();
            builder.BindCalls();
        }
    }
}