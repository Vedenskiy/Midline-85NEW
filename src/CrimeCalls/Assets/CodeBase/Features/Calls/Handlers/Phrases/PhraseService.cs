using System;
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

        public void ShowPhrase(PhraseNode node)
        {
            Debug.Log($"[phrase] {node.PersonKey} say: [{node.MessageKey}] {_localization.GetTranslatedString(node.MessageKey)} ({node.DurationInSeconds}");
            PhraseShown?.Invoke(node);
        }

        public void HidePhrase(PhraseNode node) =>
            PhraseHide?.Invoke(node);
    }
}