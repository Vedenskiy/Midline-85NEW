using CodeBase.Features.Calls.External.LogicalExpression;

namespace CodeBase.Features.Calls.Handlers.Branches
{
    public class VariablesProvider : IVariables
    {
        public int this[string variableName] => throw new System.NotImplementedException();
    }
}