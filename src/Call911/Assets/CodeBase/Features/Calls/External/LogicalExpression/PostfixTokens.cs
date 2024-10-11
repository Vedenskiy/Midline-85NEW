using System.Collections;
using System.Collections.Generic;
using CodeBase.Features.Calls.External.LogicalExpression.Operations;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Common;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Common.Brackets;

namespace CodeBase.Features.Calls.External.LogicalExpression
{
    public class PostfixTokens : IEnumerable<IToken>
    {
        private readonly IEnumerable<IToken> _tokens;
    
        public PostfixTokens(IEnumerable<IToken> infixTokens) => 
            _tokens = infixTokens;
    
        public IEnumerator<IToken> GetEnumerator() => 
            ConvertToPostfix(_tokens);

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();

        private static IEnumerator<IToken> ConvertToPostfix(IEnumerable<IToken> infixTokens)
        {
            var stackOfOperators = new Stack<IToken>();

            foreach (var token in ConvertToPostfix(infixTokens, stackOfOperators))
                yield return token;

            foreach (var op in stackOfOperators)
                yield return op;
        }

        private static IEnumerable<IToken> ConvertToPostfix(IEnumerable<IToken> infixTokens, Stack<IToken> stackOfOperators)
        {
            foreach (var token in infixTokens)
            {
                switch (token)
                {
                    case OpenBracketToken:
                        stackOfOperators.Push(token);
                        break;
                
                    case CloseBracketToken:
                        foreach (var op in TakeTokensUntilFind<OpenBracketToken>(stackOfOperators))
                            yield return op;
                        break;
                
                    case OperatorToken op:
                        if (IsThisOperatorWithLowerPriority(op, stackOfOperators))
                            yield return stackOfOperators.Pop();
                    
                        stackOfOperators.Push(op);
                        break;
                
                    default:
                        yield return token;
                        break;
                }
            }
        }

        private static IEnumerable<IToken> TakeTokensUntilFind<T>(Stack<IToken> tokens)
        {
            while (true)
            {
                var op = tokens.Pop();
                        
                if (op is T)
                    break;

                yield return op;
            }
        }

        private static bool IsThisOperatorWithLowerPriority(OperatorToken op, Stack<IToken> tokens)
        {
            if (tokens.Count <= 0) 
                return false;
        
            return tokens.Peek() is OperatorToken last 
                   && last.Precedence >= op.Precedence;
        }
    }
}