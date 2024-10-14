using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Handlers.Choices
{
    public class PlayerChoices
    {
        public event Action<IReadOnlyCollection<ChoiceData>> ChoicesShown;
        public event Action ChoicesHide; 

        public string LastChoiceId { get; private set; }
        
        public async UniTask<string> WaitChoiceSelection(IReadOnlyCollection<ChoiceData> choices, CancellationToken token)
        {
            LastChoiceId = string.Empty;
            ChoicesShown?.Invoke(choices);

            while (LastChoiceId == string.Empty) 
                await UniTask.Delay(100, cancellationToken: token);

            ChoicesHide?.Invoke();
            return LastChoiceId;
        }

        public void Choice(string choiceId)
        {
            LastChoiceId = choiceId;
        }
    }
}