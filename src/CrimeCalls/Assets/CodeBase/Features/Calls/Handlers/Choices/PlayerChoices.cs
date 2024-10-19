using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Handlers.Choices
{
    public class PlayerChoices
    {
        private readonly ChoiceTimer _choiceTimer;

        public PlayerChoices(ChoiceTimer choiceTimer) => 
            _choiceTimer = choiceTimer;

        public event Action<ICollection<ChoiceData>> ChoicesShown;
        public event Action ChoicesHide;
        public event Action<string> Chosen;

        public string LastChoiceId { get; private set; }
        
        public async UniTask<string> WaitChoiceSelection(IList<ChoiceData> choices, CancellationToken token)
        {
            LastChoiceId = string.Empty;
            ChoicesShown?.Invoke(choices);

            _choiceTimer.Start(5f);
            while (LastChoiceId == string.Empty)
            {
                await UniTask.Delay(100, cancellationToken: token);
                _choiceTimer.Update(0.1f);
                
                if (_choiceTimer.IsElapsed)
                    Choice(RandomChoiceFrom(choices));
            }
            _choiceTimer.Stop();

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