using CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary;

namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Arithmetic.Add
{
    public class AddOperationToken : OperatorToken, IBinaryOperationToken
    {
        public AddOperationToken(int precedence) 
            : base(precedence) { }
    
        public INode Create(INode left, INode right) => 
            new AddOperatorNode(left, right);
    }
}