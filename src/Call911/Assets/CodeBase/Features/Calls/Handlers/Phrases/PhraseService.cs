using System;

namespace CodeBase.Features.Calls.Handlers.Phrases
{
    public class PhraseService
    {
        public event Action<PhraseData> PhraseShown;
        public event Action<PhraseData> PhraseHide; 

        public void ShowPhrase(PhraseData data) => 
            PhraseShown?.Invoke(data);

        public void HidePhrase(PhraseData data) =>
            PhraseHide?.Invoke(data);
    }
}