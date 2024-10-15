using System;

namespace CodeBase.Features.Calls.Handlers.Phrases
{
    public class PhraseService
    {
        public event Action<PhraseNode> PhraseShown;
        public event Action<PhraseNode> PhraseHide; 

        public void ShowPhrase(PhraseNode node) => 
            PhraseShown?.Invoke(node);

        public void HidePhrase(PhraseNode node) =>
            PhraseHide?.Invoke(node);
    }
}