namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.Greater
{
    public class GreaterOperationToken : OperatorToken, IBinaryOperationToken
    {
        public GreaterOperationToken(int precedence) 
            : base(precedence) { }
    
        public INode Create(INode left, INode right) => 
            new GreaterOperatorNode(left, right);
    }
}