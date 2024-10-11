using Reflex.Core;
using UnityEngine;

namespace CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.BindCalls();
        }
    }
}