using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Features.Calls.External.LogicalExpression.Operations;
using CodeBase.Features.Calls.External.LogicalExpression.Operations.Common;

namespace CodeBase.Features.Calls.External.LogicalExpression
{
    public class LineOfTokens : IEnumerable<IToken>
    {
        private readonly ParsedString _parsedString;
        private readonly TokenFactory _tokenFactory;

        public LineOfTokens(ParsedString parsedString, TokenFactory tokenFactory)
        {
            _parsedString = parsedString;
            _tokenFactory = tokenFactory;
        }

        public IEnumerator<IToken> GetEnumerator() => 
            _parsedString
                .Select(parsedString => _tokenFactory.Parse(parsedString))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }
}   
