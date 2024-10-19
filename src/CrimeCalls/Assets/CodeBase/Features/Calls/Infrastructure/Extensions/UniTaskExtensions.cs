using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Infrastructure.Extensions
{
    public static class UniTaskExtensions
    {
        public static bool IsCompleted(this UniTask task) => 
            task.Status != UniTaskStatus.Pending;
    }
}