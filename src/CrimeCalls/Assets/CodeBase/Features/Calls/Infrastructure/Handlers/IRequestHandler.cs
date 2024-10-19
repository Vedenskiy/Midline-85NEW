using System.Threading;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Infrastructure.Handlers
{
    public interface IRequestHandler
    {
        UniTask Handle(object request, CancellationToken token = default);
    }
}