using System.Collections.Generic;
using CodeBase.Features.Calls.External.LogicalExpression;

namespace CodeBase.Features.Calls.Handlers.Variables
{
    public class VariablesProvider : IVariables
    {
        private readonly Dictionary<string, int> _variables;

        public int this[string variableName]
        {
            get => _variables[variableName];
            set => _variables[variableName] = value;
        }
    }
}