using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases.UI
{
    public class PhrasesUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private CallPhraseView _callPhrasePrefab;

        private readonly Dictionary<string, CallPhraseView> _phraseViews = new();
        private PhraseService _phrases;
        private bool _isPreviousToRightMapped = false;

        [Inject]
        public void Construct(PhraseService phrases) => 
            _phrases = phrases;

        private void OnEnable() => 
            _phrases.PhraseShown += OnPhraseShown;

        private void OnDisable() => 
            _phrases.PhraseShown -= OnPhraseShown;

        private void OnPhraseShown(PhraseData data)
        {
            if (_phraseViews.TryGetValue(data.PersonKey, out var view))
            {
                view.Setup(data.PersonKey, data.MessageKey);
            }
            else
            {
                var instance = CreateNewCallPhrase();
                instance.Setup(data.PersonKey, data.MessageKey);
                _phraseViews[data.PersonKey] = instance;
            }
        }

        private CallPhraseView CreateNewCallPhrase()
        {
            var instance = Instantiate(_callPhrasePrefab, _container);
            instance.GetComponent<CallPhraseAlign>().Align(_isPreviousToRightMapped);
            _isPreviousToRightMapped = !_isPreviousToRightMapped;
            return instance;
        }
    }
}