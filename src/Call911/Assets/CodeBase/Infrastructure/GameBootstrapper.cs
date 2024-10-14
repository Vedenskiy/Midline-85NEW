using System.Collections.Generic;
using CodeBase.Features.Calls.Handlers.Phrases;
using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using Reflex.Attributes;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private CallsExecutor _executor;
        private NodeRepository _nodes;
        
        [Inject]
        public void Construct(CallsExecutor executor, NodeRepository nodes)
        {
            _executor = executor;
            _nodes = nodes;
        }

        private async void Start()
        {
            _nodes.Load(GetTestPhrases(), GetTestLinks());
            await _executor.Execute("0");
        }

        private IEnumerable<NodeLink> GetTestLinks()
        {
            yield return Link("0", "1");
            yield return Link("1", "2");
            yield return Link("2", "3");
            yield return Link("2", "4");
        }

        private IEnumerable<Node> GetTestPhrases()
        {
            yield return ElenaSay("Hello, World!", "0");
            yield return MarkSay("Hello, Elena!", "1");
            yield return ElenaSay("How are you?", "2");
            yield return MarkSay("I'm fine, how you?", "3");
        }

        private NodeLink Link(string parent, string child) => 
            new() { ParentId = parent, ChildId = child };

        private Node ElenaSay(string message, string withId) => 
            new PhraseData() { Guid = withId, PersonKey = "Elena", MessageKey = message, DurationInSeconds = 2f };

        private Node MarkSay(string message, string withId) =>
            new PhraseData() { Guid = withId, PersonKey = "Mark", MessageKey = message, DurationInSeconds = 2f };
    }
}