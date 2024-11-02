using System;
using CodeBase.Common.Extensions;
using CodeBase.Infrastructure.Common.Localization;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases
{
    public class PhraseService
    {
        private readonly LocalizationService _localization;

        public PhraseService(LocalizationService localization) => 
            _localization = localization;

        public event Action<PhraseNode> PhraseShown;
        public event Action<PhraseNode> PhraseHide;

        public void ShowPhrase(PhraseNode phrase)
        {
            Log(phrase);
            PhraseShown?.Invoke(phrase);
        }

        public void HidePhrase(PhraseNode phrase) =>
            PhraseHide?.Invoke(phrase);

        private void Log(PhraseNode phrase)
        {
            var tag = Color.cyan.Paint("phrase");
            Debug.Log($"[{tag}] {phrase.PersonKey} say: [{phrase.MessageKey}] {_localization.GetTranslatedString(phrase.MessageKey)} ({phrase.DurationInSeconds}s)");
        }
    }
}