using System.Collections.Generic;
using System.Text;
using CodeBase.Features.Calls.External.LogicalExpression;

namespace CodeBase.Features.Calls.Handlers.Variables
{
    public class VariablesProvider : IVariables
    {
        private readonly Dictionary<string, int> _variables;

        public VariablesProvider() => 
            _variables = new Dictionary<string, int>();

        public int this[string variableName]
        {
            get => _variables[variableName];
            set => _variables[variableName] = value;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var pair in _variables) 
                builder.Append($"{pair.Key} = {pair.Value};");

            return builder.ToString();
        }
    }
}