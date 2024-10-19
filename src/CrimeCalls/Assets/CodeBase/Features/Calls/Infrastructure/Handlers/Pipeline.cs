using System;
using System.Collections.Generic;
using System.Threading;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Infrastructure.Handlers
{
    public class Pipeline
    {
        private readonly IDictionary<Type, IRequestHandler> _handlers;

        public Pipeline(IDictionary<Type, IRequestHandler> handlers) => 
            _handlers = handlers;

        public UniTask Execute<TRequest>(TRequest request, CancellationToken token = default) where TRequest : Node
        {
            var requestType = request.GetType();
            var handler = _handlers[requestType];
            return handler.Handle(request, token);
        }
    }
}
