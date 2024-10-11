using System.Threading;
using CodeBase.Features.Calls.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Features.Calls.Phrases
{
    public class PhraseHandler : RequestHandler<PhraseData>
    {
        protected override async UniTask Handle(PhraseData request, CancellationToken token)
        {
            Debug.Log($"{request.PersonKey} say: \"{request.MessageKey}\"");
            await UniTask.Delay(FromSecondToMilliseconds(request.DurationInSeconds), cancellationToken: token);
        }

        private static int FromSecondToMilliseconds(float seconds) => 
            (int)(seconds * 1000);
    }
}