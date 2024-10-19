namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Unary.Not
{
    public class NotOperationToken : OperatorToken, IUnaryOperationToken
    {
        public NotOperationToken(int precedence) 
            : base(precedence) { }

        public INode Create(INode argument) => 
            new NotOperatorNode(argument);
    }
}