using System.Threading;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Infrastructure
{
    public interface IRequestHandler
    {
        UniTask Handle(object request, CancellationToken token = default);
    }
}