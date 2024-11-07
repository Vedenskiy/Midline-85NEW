using CodeBase.Common.Tweens;
using FronkonGames.TinyTween;
using FronkonGames.TinyTween.Easing;
using TMPro;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases.UI
{
    public class CallPhraseView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvas;
        
        [SerializeField] private TextMeshProUGUI _personName;
        [SerializeField] private TextMeshProUGUI _personMessage;

        [SerializeField] private CallPhraseBlur _blur;

        private bool _isHighlighted;
        
        public void Setup(string personKey, string messageKey)
        {
            _personName.text = personKey.ToUpper();
            _personMessage.text = messageKey;
        }

        public void Highlight()
        {
            if (_isHighlighted)
                return;
            
            _canvas.TweenAlpha(0.3f, 1f, 1f, Ease.Linear);
            _blur.ShowMessage();
            _isHighlighted = true;
        }

        public void Unhighlight()
        {
            if (!_isHighlighted)
                return;
            
            _canvas.TweenAlpha(1f, 0.3f, 1f, Ease.Linear);
            _blur.HideMessage();
            _isHighlighted = false;
        }
    }
}