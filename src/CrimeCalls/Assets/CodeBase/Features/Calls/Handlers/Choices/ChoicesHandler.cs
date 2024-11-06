using System.Threading;
using CodeBase.Common.Extensions;
using CodeBase.Features.Calls.Audio;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Choices
{
    public class ChoicesHandler : RequestHandler<ChoicesNode>
    {
        private readonly CallsAudioService _audio;
        private readonly PlayerChoices _choices;

        public ChoicesHandler(CallsAudioService audio, PlayerChoices choices)
        {
            _audio = audio;
            _choices = choices;
        }

        protected override async UniTask Handle(ChoicesNode request, CancellationToken token)
        {
            _audio.PlayTimerSfx();
            await _choices.WaitChoiceSelection(request.Choices, token);
            Debug.Log($"[{Color.magenta.Paint("choices")}] player choice: {_choices.LastChoiceId}");
            _audio.StopTimerSfx();
        }
    }
}