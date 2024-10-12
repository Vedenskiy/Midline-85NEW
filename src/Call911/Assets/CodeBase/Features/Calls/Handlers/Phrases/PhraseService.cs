using System;

namespace CodeBase.Features.Calls.Handlers.Phrases
{
    public class PhraseService
    {
        public event Action<PhraseData> PhraseShown; 
        
        public void ShowPhrase(PhraseData data) => 
            PhraseShown?.Invoke(data);
    }
}