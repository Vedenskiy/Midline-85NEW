using CodeBase.Features.Calls.External.LogicalExpression.Operations.Common;

namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary
{
    public interface IBinaryOperationToken : IToken
    {
        INode Create(INode left, INode right);
    }
}