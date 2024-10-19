namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.And
{
    public class AndOperationToken : OperatorToken, IBinaryOperationToken
    {
        public AndOperationToken(int precedence) 
            : base(precedence) { }
    
        public INode Create(INode left, INode right) => 
            new AndOperatorNode(left, right);
    }
}