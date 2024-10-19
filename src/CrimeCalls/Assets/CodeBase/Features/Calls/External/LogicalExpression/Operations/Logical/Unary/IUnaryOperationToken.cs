using CodeBase.Features.Calls.External.LogicalExpression.Operations.Common;

namespace CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Unary
{
    public interface IUnaryOperationToken : IToken
    {
        INode Create(INode argument);
    }
}