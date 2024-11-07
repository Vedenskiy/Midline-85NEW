using System.Threading;
using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls
{
    public class CallExecutor
    {
        private readonly NodeExecutor _nodeExecutor;
        private readonly NodeRepository _nodes;
        private readonly CallRepository _calls;

        public CallExecutor(
            NodeExecutor nodeExecutor, 
            NodeRepository nodes, 
            CallRepository calls)
        {
            _nodeExecutor = nodeExecutor;
            _nodes = nodes;
            _calls = calls;
        }

        public async UniTask Execute(string callName, CancellationToken token = default)
        {
            var call = _calls.GetByName(callName);
            _nodes.Load(call.GetAllNodes(), call.Links);
            await _nodeExecutor.Execute(_nodes.GetById(call.EntryNodeId), token);
        }
    }
}