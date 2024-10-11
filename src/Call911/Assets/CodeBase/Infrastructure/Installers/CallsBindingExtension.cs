using System;
using System.Collections.Generic;
using CodeBase.Features.Calls.Handlers.Phrases;
using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using CodeBase.Infrastructure.Installers.Extensions;
using Reflex.Core;

namespace CodeBase.Infrastructure.Installers
{
    public static class CallsBindingExtension
    {
        public static ContainerBuilder BindCalls(this ContainerBuilder builder)
        {
            builder.BindHandlers();
            builder.BindInfrastructure();
            
            return builder;
        }

        private static void BindHandlers(this ContainerBuilder builder)
        {
            builder.AddSingleton<PhraseHandler>();
        }

        private static void BindInfrastructure(this ContainerBuilder builder)
        {
            builder.AddSingleton(container => new Pipeline(new Dictionary<Type, IRequestHandler>()
            {
                [typeof(PhraseData)] = container.Resolve<PhraseHandler>()
            }));
            
            builder.AddSingleton<NodeRepository>();
            builder.AddSingleton<ParallelExecutor>();
        }
    }
}