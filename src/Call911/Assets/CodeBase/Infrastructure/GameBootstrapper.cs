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
            await _executor.Execute(new PhraseData()
                { PersonKey = "Elena", MessageKey = "Hello, World!", DurationInSeconds = 2 });
        }
    }
}