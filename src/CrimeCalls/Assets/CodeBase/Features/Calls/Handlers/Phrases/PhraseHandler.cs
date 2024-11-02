using System.Linq;
using System.Threading;
using CodeBase.Common.Extensions;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases
{
    public class PhraseHandler : RequestHandler<PhraseNode>
    {
        private const float TechnicalPauseDuration = 2f;
        private readonly PhraseService _phrases;
        private readonly NodeRepository _nodes;

        public PhraseHandler(PhraseService phrases, NodeRepository nodes)
        {
            _phrases = phrases;
            _nodes = nodes;
        }

        protected override async UniTask Handle(PhraseNode request, CancellationToken token)
        {
            _phrases.ShowPhrase(request);
            await AfterPhrasePause(request, token);

            if (IsNextNodeHasSamePerson(request))
            {
                Debug.Log($"{Color.green.Paint("Next phrase node says same person: ")}{request.PersonKey}");
                return;
            }
            
            _phrases.HidePhrase(request);
        }

        private static UniTask AfterPhrasePause(PhraseNode request, CancellationToken token) => 
            UniTask.Delay(FromSecondToMilliseconds(request.DurationInSeconds + TechnicalPauseDuration), cancellationToken: token);

        private static int FromSecondToMilliseconds(float seconds) => 
            (int)(seconds * 1000);

        private bool IsNextNodeHasSamePerson(PhraseNode node)
        {
            var nextNodes = _nodes.GetChildrenFrom(node.Guid);
            return nextNodes.Any(nextNode => nextNode is PhraseNode phraseNode && phraseNode.PersonKey == node.PersonKey);
        }
    }
}