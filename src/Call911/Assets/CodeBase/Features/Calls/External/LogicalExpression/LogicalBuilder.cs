using CodeBase.Features.Calls.External.LogicalExpression.Operations;

namespace CodeBase.Features.Calls.External.LogicalExpression
{
    public static class LogicalBuilder
    {
        public static LogicalExpression Expression(string input) =>
            new
            (
                new PostfixTokens
                (
                    new LineOfTokens
                    (
                        new ParsedString(input), 
                        new TokenFactory()
                    )
                )
            );
    }
}