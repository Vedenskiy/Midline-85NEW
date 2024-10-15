using System.Threading;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Infrastructure.Handlers
{
    public class NoOperationHandler : IRequestHandler
    {
        public UniTask Handle(object request, CancellationToken token = default) => 
            UniTask.CompletedTask;
    }
}