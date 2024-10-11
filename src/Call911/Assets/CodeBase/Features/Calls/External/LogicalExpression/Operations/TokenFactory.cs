using CodeBase.Features.Calls.External.LogicalExpression.Operations.Arithmetic.Add;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Arithmetic.Subtract;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Common;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Common.Brackets;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Common.Constant;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Common.Variable;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.And;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.Equal;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.Greater;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.Less;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Logical.Binary.Or;

namespace CodeBase.Features.Calls.External.LogicalExpression.Operations
{
    public class TokenFactory
    {
        public IToken Parse(string content) =>
            content.ToLower() switch
            {
                "(" => new OpenBracketToken(),
                ")" => new CloseBracketToken(),
                "-" => new SubtractOperationToken(3),
                "+" => new AddOperationToken(3),
                ">" => new GreaterOperationToken(2),
                "<" => new LessOperationToken(2),
                "=" or "==" => new EqualOperationToken(2),
                "|" or "||" => new OrOperationToken(1),
                "&" or "&&" => new AndOperationToken(1),
                _ => ParseVariableOrConstant(content)
            };

        private static IToken ParseVariableOrConstant(string content) => 
            int.TryParse(content, out var value) 
                ? new ConstantToken(value) 
                : new VariableToken(content);
    }
}