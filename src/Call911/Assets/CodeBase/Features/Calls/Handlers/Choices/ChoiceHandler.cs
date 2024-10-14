using System.Collections.Generic;
using System.Threading;
using CodeBase.Features.Calls.Audio;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Handlers.Choices
{
    public class ChoicesData : Node
    {
        public List<ChoiceData> Choices;
    }

    public class ChoiceData
    {
        public string ChoiceId;
        public bool IsLocked;
        public string UnlockedCondition;
    }

    public class PlayerChoices
    {
        public string LastChoiceId { get; private set; }
        
        public async UniTask<string> WaitChoiceSelection(IReadOnlyCollection<ChoiceData> choices)
        {
            return await UniTask.FromResult(string.Empty);
        }
    }
    
    public class ChoiceHandler : RequestHandler<ChoicesData>
    {
        private readonly CallsAudioService _audio;
        private readonly PlayerChoices _choices;

        public ChoiceHandler(CallsAudioService audio, PlayerChoices choices)
        {
            _audio = audio;
            _choices = choices;
        }

        public string ChosenId { get; private set; }
        
        protected override async UniTask Handle(ChoicesData request, CancellationToken token)
        {
            _audio.PlayTimerSfx();
            ChosenId = await _choices.WaitChoiceSelection(request.Choices);
            _audio.StopTimerSfx();
        }
    }
}