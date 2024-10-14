using System.Threading;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases
{
    public class PhraseHandler : RequestHandler<PhraseData>
    {
        private readonly PhraseService _phrases;

        public PhraseHandler(PhraseService phrases) => 
            _phrases = phrases;

        protected override async UniTask Handle(PhraseData request, CancellationToken token)
        {
            _phrases.ShowPhrase(request);
            await AfterPhrasePause(request, token);
        }

        private static UniTask AfterPhrasePause(PhraseData request, CancellationToken token) => 
            UniTask.Delay(FromSecondToMilliseconds(request.DurationInSeconds), cancellationToken: token);

        private static int FromSecondToMilliseconds(float seconds) => 
            (int)(seconds * 1000);
    }
}