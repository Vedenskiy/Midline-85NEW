using TMPro;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases.UI
{
    public class CallPhraseView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvas;
        
        [SerializeField] private TextMeshProUGUI _personName;
        [SerializeField] private TextMeshProUGUI _personMessage;

        public void Setup(string personKey, string messageKey)
        {
            _personName.text = personKey.ToUpper();
            _personMessage.text = messageKey;
        }

        public void Highlight()
        {
            _canvas.alpha = 1.0f;
        }

        public void Unhighlight()
        {
            _canvas.alpha = 0.3f;
        }
    }
}