using System.Threading;
using CodeBase.Features.Calls.Audio;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Handlers.Choices
{
    public class ChoicesHandler : RequestHandler<ChoicesData>
    {
        private readonly CallsAudioService _audio;
        private readonly PlayerChoices _choices;

        public ChoicesHandler(CallsAudioService audio, PlayerChoices choices)
        {
            _audio = audio;
            _choices = choices;
        }

        protected override async UniTask Handle(ChoicesData request, CancellationToken token)
        {
            _audio.PlayTimerSfx();
            await _choices.WaitChoiceSelection(request.Choices, token);
            _audio.StopTimerSfx();
        }
    }
}