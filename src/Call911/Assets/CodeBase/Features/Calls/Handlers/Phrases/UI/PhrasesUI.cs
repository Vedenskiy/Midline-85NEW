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

        private CallPhraseView _previousPhrase;

        [Inject]
        public void Construct(PhraseService phrases) => 
            _phrases = phrases;

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

        private void OnPhraseHide(PhraseData data)
        {
            if (_phraseViews.TryGetValue(data.PersonKey, out var view)) 
                view.Unhighlight();
        }

        private void OnPhraseShown(PhraseData data)
        {
            if (!_phraseViews.ContainsKey(data.PersonKey))
                _phraseViews[data.PersonKey] = CreateNewCallPhrase();

            var nextPhraseView = _phraseViews[data.PersonKey];
            
            if (_previousPhrase != nextPhraseView && _previousPhrase != null)
                _previousPhrase.Unhighlight();

            _previousPhrase = nextPhraseView;
            _previousPhrase.Setup(data.PersonKey, data.MessageKey);
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