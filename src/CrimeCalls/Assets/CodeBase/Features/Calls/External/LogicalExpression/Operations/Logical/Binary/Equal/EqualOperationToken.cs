namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.Equal
{
    public class EqualOperationToken : OperatorToken, IBinaryOperationToken
    {
        public EqualOperationToken(int precedence) 
            : base(precedence) { }

        public INode Create(INode left, INode right) => 
            new EqualOperatorNode(left, right);
    }
}