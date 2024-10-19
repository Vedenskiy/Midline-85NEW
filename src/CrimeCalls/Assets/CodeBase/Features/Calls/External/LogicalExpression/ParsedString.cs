using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CodeBase.Features.Calls.External.LogicalExpression
{
    public class ParsedString : IEnumerable<string>
    {
        private const int IgnoreWhitespacesGroup = 1;
    
        private readonly Regex _regex = new Regex(@"([\w]+|[><=&-+-!|]+|=>)\s*");
        private readonly string _input;

        public ParsedString(string input) => 
            _input = input;

        public IEnumerator<string> GetEnumerator()
        {
            foreach (Match match in _regex.Matches(_input))
            {
                if (!match.Success)
                    throw new Exception($"Invalid token at position {match.Index}");

                yield return match.Groups[IgnoreWhitespacesGroup].Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }
}