namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Common.Variable
{
    public class VariableToken : IEmptyOperationToken
    {
        public VariableToken(string name) => 
            Name = name;

        public string Name { get; }
    
        public INode Create() => 
            new VariableNode(Name);
    }
}