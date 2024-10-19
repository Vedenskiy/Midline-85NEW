using System.Threading;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Infrastructure.Handlers
{
    public abstract class RequestHandler<TRequest> : IRequestHandler where TRequest : Node
    {
        UniTask IRequestHandler.Handle(object request, CancellationToken token) => 
            Handle((TRequest)request, token);

        protected abstract UniTask Handle(TRequest request, CancellationToken token);
    }
}