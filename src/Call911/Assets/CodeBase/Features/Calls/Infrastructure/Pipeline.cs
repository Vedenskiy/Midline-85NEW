using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Infrastructure
{
    public class Pipeline
    {
        private readonly IDictionary<Type, IRequestHandler> _handlers;

        public Pipeline(IDictionary<Type, IRequestHandler> handlers) => 
            _handlers = handlers;

        public UniTask Execute<TRequest>(TRequest request, CancellationToken token = default)
        {
            var requestType = typeof(TRequest);
            var handler = _handlers[requestType];
            return handler.Handle(request, token);
        }
    }
}
