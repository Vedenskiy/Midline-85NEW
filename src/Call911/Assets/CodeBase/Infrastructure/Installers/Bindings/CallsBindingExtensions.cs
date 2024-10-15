using System;
using System.Collections.Generic;
using CodeBase.Features.Calls.Audio;
using CodeBase.Features.Calls.External.LogicalExpression;
using CodeBase.Features.Calls.Handlers.Branches;
using CodeBase.Features.Calls.Handlers.Choices;
using CodeBase.Features.Calls.Handlers.Phrases;
using CodeBase.Features.Calls.Handlers.Variables;
using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using CodeBase.Infrastructure.Installers.Extensions;
using Reflex.Core;

namespace CodeBase.Infrastructure.Installers.Bindings
{
    public static class CallsBindingExtensions
    {
        public static ContainerBuilder BindCalls(this ContainerBuilder builder)
        {
            BindHandlers(builder);
            BindInfrastructure(builder);
            
            return builder;
        }

        private static void BindHandlers(this ContainerBuilder builder)
        {
            builder.AddSingleton<PhraseService>();
            builder.AddSingleton<PhraseHandler>();

            builder.AddSingleton<CallsAudioService>();
            builder.AddSingleton<PlayerChoices>();
            builder.AddSingleton<ChoicesHandler>();
        }

        private static void BindInfrastructure(this ContainerBuilder builder)
        {
            builder.AddSingleton(container => new Pipeline(new Dictionary<Type, IRequestHandler>()
            {
                [typeof(PhraseData)] = container.Resolve<PhraseHandler>(),
                [typeof(ChoicesData)] = container.Resolve<ChoicesHandler>(),
                [typeof(VariableNode)] = container.Resolve<VariableHandler>(),
            }));

            builder.AddSingleton(typeof(VariablesProvider), typeof(IVariables));
            
            builder.AddSingleton<NodeRepository>();
            builder.AddSingleton<BranchEvaluator>();
            builder.AddSingleton<NodeScheduler>();
            builder.AddSingleton<CallsExecutor>();
        }
    }
}