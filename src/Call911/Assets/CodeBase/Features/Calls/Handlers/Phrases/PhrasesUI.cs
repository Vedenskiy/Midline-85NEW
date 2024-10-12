using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases
{
    public class PhrasesUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;
        
        private PhraseService _phrases;

        [Inject]
        public void Construct(PhraseService phrases) => 
            _phrases = phrases;

        private void OnEnable() => 
            _phrases.PhraseShown += OnPhraseShown;

        private void OnDisable() => 
            _phrases.PhraseShown -= OnPhraseShown;

        private void OnPhraseShown(PhraseData data)
        {
            _label.text = $"{data.PersonKey.ToUpper()}: {data.MessageKey}";
        }
    }
}