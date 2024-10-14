using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Handlers.Choices
{
    public class PlayerChoices
    {
        public string LastChoiceId { get; private set; }
        
        public async UniTask<string> WaitChoiceSelection(IReadOnlyCollection<ChoiceData> choices)
        {
            return await UniTask.FromResult(string.Empty);
        }
    }
}