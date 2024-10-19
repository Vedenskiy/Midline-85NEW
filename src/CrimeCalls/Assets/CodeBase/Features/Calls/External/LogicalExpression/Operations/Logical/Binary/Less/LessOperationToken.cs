namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.Less
{
    public class LessOperationToken : OperatorToken, IBinaryOperationToken
    {
        public LessOperationToken(int precedence) 
            : base(precedence) { }

        public INode Create(INode left, INode right) => 
            new LessOperatorNode(left, right);
    }
}