using System.Threading;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases
{
    public class PhraseHandler : RequestHandler<PhraseNode>
    {
        private const float TechnicalPauseDuration = 2f;
        private readonly PhraseService _phrases;

        public PhraseHandler(PhraseService phrases) => 
            _phrases = phrases;

        protected override async UniTask Handle(PhraseNode request, CancellationToken token)
        {
            _phrases.ShowPhrase(request);
            await AfterPhrasePause(request, token);
            _phrases.HidePhrase(request);
        }

        private static UniTask AfterPhrasePause(PhraseNode request, CancellationToken token) => 
            UniTask.Delay(FromSecondToMilliseconds(request.DurationInSeconds + TechnicalPauseDuration), cancellationToken: token);

        private static int FromSecondToMilliseconds(float seconds) => 
            (int)(seconds * 1000);
    }
}