using System.Collections.Generic;
using CodeBase.Features.Calls.Handlers.Choices;
using CodeBase.Features.Calls.Handlers.Phrases;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using Reflex.Attributes;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Pipeline _executor;
        
        [Inject]
        public void Construct(Pipeline executor) => 
            _executor = executor;

        private async void Start()
        {
            foreach (var phraseData in GetTestPhrases()) 
                await _executor.Execute(phraseData);
        }

        private IEnumerable<Node> GetTestPhrases()
        {
            yield return new ChoicesData()
            {
                Choices = new List<ChoiceData>() { new() { ChoiceId = "1" }, new() { ChoiceId = "2" }, new() { ChoiceId = "3" } }
            };
            
            yield return new PhraseData() { PersonKey = "Elena", MessageKey = "Hello, world!", DurationInSeconds = 2 };
            yield return new PhraseData() { PersonKey = "Mark", MessageKey = "Hello, Elena!", DurationInSeconds = 2 };
            yield return new PhraseData() { PersonKey = "Elena", MessageKey = "Second message!", DurationInSeconds = 2 };
            yield return new PhraseData() { PersonKey = "Elena", MessageKey = "Third message!", DurationInSeconds = 2 };
            
            yield return new ChoicesData()
            {
                Choices = new List<ChoiceData>() { new() { ChoiceId = "4" }, new() { ChoiceId = "5" }, new() { ChoiceId = "6" } }
            };
            
            yield return new PhraseData() { PersonKey = "Elena", MessageKey = "Oops, it's all!", DurationInSeconds = 2 };
        }
    }
}