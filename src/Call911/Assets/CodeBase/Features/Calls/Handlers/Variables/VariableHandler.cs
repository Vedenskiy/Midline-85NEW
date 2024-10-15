using System.Threading;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Handlers.Variables
{
    public class VariableHandler : RequestHandler<VariableNode>
    {
        private readonly VariablesProvider _variables;

        public VariableHandler(VariablesProvider variables) => 
            _variables = variables;

        protected override UniTask Handle(VariableNode request, CancellationToken token)
        {
            _variables[request.VariableName] = request.Value;
            return UniTask.CompletedTask;
        }
    }
}