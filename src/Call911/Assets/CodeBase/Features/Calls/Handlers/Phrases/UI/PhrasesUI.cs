using System.Collections.Generic;
using CodeBase.Infrastructure.Common.Localization;
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
        private LocalizationService _localization;
        
        private bool _isPreviousToRightMapped = false;

        private CallPhraseView _previousPhrase;

        [Inject]
        public void Construct(PhraseService phrases, LocalizationService localizationService)
        {
            _phrases = phrases;
            _localization = localizationService;
        }

        private void OnEnable()
        {
            _phrases.PhraseShown += OnPhraseShown;
            _phrases.PhraseHide += OnPhraseHide;
        }

        private void OnDisable()
        {
            _phrases.PhraseShown -= OnPhraseShown;
            _phrases.PhraseHide -= OnPhraseHide;
        }

        private void OnPhraseHide(PhraseNode node)
        {
            if (_phraseViews.TryGetValue(node.PersonKey, out var view)) 
                view.Unhighlight();
        }

        private void OnPhraseShown(PhraseNode node)
        {
            if (!_phraseViews.ContainsKey(node.PersonKey))
                _phraseViews[node.PersonKey] = CreateNewCallPhrase();

            var nextPhraseView = _phraseViews[node.PersonKey];
            
            _previousPhrase = nextPhraseView;
            _previousPhrase.Setup(
                _localization.GetTranslatedString(node.PersonKey), 
                _localization.GetTranslatedString(node.MessageKey));
            _previousPhrase.Highlight();
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