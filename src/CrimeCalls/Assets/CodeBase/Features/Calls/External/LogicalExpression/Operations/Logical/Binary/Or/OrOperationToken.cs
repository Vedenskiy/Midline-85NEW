namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.Or
{
    public class OrOperationToken : OperatorToken, IBinaryOperationToken
    {
        public OrOperationToken(int precedence) 
            : base(precedence) { }
    
        public INode Create(INode left, INode right) => 
            new OrOperatorNode(left, right);
    }
}