using System.Collections.Generic;
using System.Linq;
using CodeBase.Features.Calls.Handlers.Choices;
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
            await _executor.Execute(GetTestPhrases().First());
            Debug.Log("Level Completed!");
        }

        private IEnumerable<NodeLink> GetTestLinks()
        {
            yield return Link("0", "1");
            yield return Link("1", "2");
            yield return Link("2", "3");
            yield return Link("3", "4");
            
            yield return Link("choice_1", "5");
            yield return Link("choice_2", "6");
            yield return Link("choice_3", "7");
            
            yield return Link("5", "8");
            yield return Link("6", "9");
            yield return Link("7", "10");
        }

        private IEnumerable<Node> GetTestPhrases()
        {
            yield return ElenaSay("Hello, World!", "0");
            yield return MarkSay("Hello, Elena!", "1");
            yield return ElenaSay("How are you?", "2");
            yield return MarkSay("I'm fine, how you?", "3");
            yield return new ChoicesNode()
            {
                Guid = "4",
                Choices = new List<ChoiceData>()
                {
                    new ChoiceData() { ChoiceId = "choice_1"},
                    new ChoiceData() { ChoiceId = "choice_2"},
                    new ChoiceData() { ChoiceId = "choice_3"},
                }
            };
            
            yield return ElenaSay("I'm, CHOICE 1!", "5");
            yield return ElenaSay("I'm, CHOICE 2!", "6");
            yield return ElenaSay("I'm, CHOICE 3!", "7");
            
            yield return MarkSay("Wow, choice 1!", "8");
            yield return MarkSay("Wow, choice 2!", "9");
            yield return MarkSay("Wow, choice 3!", "10");
        }

        private NodeLink Link(string parent, string child) => 
            new() { ParentId = parent, ChildId = child };

        private Node ElenaSay(string message, string withId) => 
            new PhraseNode() { Guid = withId, PersonKey = "Elena", MessageKey = message, DurationInSeconds = 2f };

        private Node MarkSay(string message, string withId) =>
            new PhraseNode() { Guid = withId, PersonKey = "Mark", MessageKey = message, DurationInSeconds = 2f };
    }
}