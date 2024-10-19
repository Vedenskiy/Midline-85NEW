using CodeBase.Features.Calls.External.LogicalExpression.Operations.Common;

namespace CodeBase.Features.Calls.External.LogicalExpression.Operations
{
    public class OperatorToken : IToken
    {
        public OperatorToken(int precedence) => 
            Precedence = precedence;

        public int Precedence { get; }
    }
}