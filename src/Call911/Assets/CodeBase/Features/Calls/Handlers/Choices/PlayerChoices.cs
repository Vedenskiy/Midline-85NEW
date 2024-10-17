using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Handlers.Choices
{
    public class PlayerChoices
    {
        public event Action<ICollection<ChoiceData>> ChoicesShown;
        public event Action ChoicesHide;
        public event Action<string> Chosen;
        public event Action<float> TimerStarted; 

        public string LastChoiceId { get; private set; }
        
        public async UniTask<string> WaitChoiceSelection(IList<ChoiceData> choices, CancellationToken token)
        {
            LastChoiceId = string.Empty;
            ChoicesShown?.Invoke(choices);

            var elapsedTime = 0f;
            TimerStarted?.Invoke(2f);
            while (LastChoiceId == string.Empty)
            {
                await UniTask.Delay(100, cancellationToken: token);
                elapsedTime += 0.1f;
                if (elapsedTime > 2f) 
                    Choice(RandomChoiceFrom(choices));
            }

            ChoicesHide?.Invoke();
            return LastChoiceId;
        }

        public void Choice(string choiceId)
        {
            LastChoiceId = choiceId;
            Chosen?.Invoke(choiceId);
        }

        private static string RandomChoiceFrom(IList<ChoiceData> choices)
        {
            var randomIndex = UnityEngine.Random.Range(0, choices.Count);
            var choiceData = choices[randomIndex];
            return choiceData.ChoiceId;
        }
    }
}