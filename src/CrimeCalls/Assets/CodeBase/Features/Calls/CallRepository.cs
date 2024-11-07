using System.Collections.Generic;
using CodeBase.Features.Calls.Infrastructure;

namespace CodeBase.Features.Calls
{
    public class CallRepository
    {
        private readonly Dictionary<string, Dialogue> _calls = new();

        public Dialogue GetByName(string name) => 
            _calls[name];

        public bool Contains(string name) =>
            _calls.ContainsKey(name);

        public void Add(string callName, Dialogue dialogue) => 
            _calls.Add(callName, dialogue);
    }
}