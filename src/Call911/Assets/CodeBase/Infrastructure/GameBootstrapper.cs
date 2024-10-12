using System.Collections.Generic;
using CodeBase.Features.Calls.Handlers.Phrases;
using CodeBase.Features.Calls.Infrastructure.Handlers;
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

        private IEnumerable<PhraseData> GetTestPhrases()
        {
            yield return new PhraseData() { PersonKey = "Elena", MessageKey = "Hello, world!", DurationInSeconds = 2 };
            yield return new PhraseData() { PersonKey = "Elena", MessageKey = "Second message!", DurationInSeconds = 2 };
            yield return new PhraseData() { PersonKey = "Elena", MessageKey = "Third message!", DurationInSeconds = 2 };
            yield return new PhraseData() { PersonKey = "Elena", MessageKey = "Oops, it's all!", DurationInSeconds = 2 };
        }
    }
}