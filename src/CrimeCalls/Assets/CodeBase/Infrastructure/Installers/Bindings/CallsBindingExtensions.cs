using System;
using System.Collections.Generic;
using CodeBase.Features.Calls;
using CodeBase.Features.Calls.Audio;
using CodeBase.Features.Calls.External.LogicalExpression;
using CodeBase.Features.Calls.Handlers.Choices;
using CodeBase.Features.Calls.Handlers.Images;
using CodeBase.Features.Calls.Handlers.Phrases;
using CodeBase.Features.Calls.Handlers.Variables;
using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using CodeBase.Features.Calls.Infrastructure.Nodes.Branches;
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
            builder.AddSingleton<ChoiceTimer>();
            builder.AddSingleton<PlayerChoices>();
            builder.AddSingleton<ChoicesHandler>();

            builder.AddSingleton<VariableHandler>();
            builder.AddSingleton<NoOperationHandler>();

            builder.AddSingleton<ImagesService>();
            builder.AddSingleton<ImageHandler>();
        }

        private static void BindInfrastructure(this ContainerBuilder builder)
        {
            builder.AddSingleton<DialogueLoader>();
            builder.AddSingleton<DialogueGraphAdapter>();
            
            builder.AddSingleton(typeof(VariablesProvider), typeof(IVariables), typeof(VariablesProvider));

            builder.AddSingleton(container => new Pipeline(new Dictionary<Type, IRequestHandler>()
            {
                [typeof(PhraseNode)] = container.Resolve<PhraseHandler>(),
                [typeof(ChoicesNode)] = container.Resolve<ChoicesHandler>(),
                [typeof(VariableNode)] = container.Resolve<VariableHandler>(),
                [typeof(ImageNode)] = container.Resolve<ImageHandler>(),
                [typeof(Node)] = container.Resolve<NoOperationHandler>()
            }));
            
            builder.AddSingleton<NodeRepository>();
            builder.AddSingleton<BranchEvaluator>();
            builder.AddSingleton<NodeScheduler>();
            builder.AddSingleton<NodeExecutor>();

            builder.AddSingleton<CallRepository>();
            builder.AddSingleton<CallExecutor>();
        }
    }
}