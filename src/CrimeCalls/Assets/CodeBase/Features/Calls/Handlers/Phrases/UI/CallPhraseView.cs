using CodeBase.Common.Tweens;
using FronkonGames.TinyTween;
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
        
        public void Setup(string personKey, string messageKey)
        {
            _personName.text = personKey.ToUpper();
            _personMessage.text = messageKey;
        }

        public void Highlight()
        {
            _canvas.TweenAlpha(0.3f, 1f, 1f, Ease.Linear);
            _blur.ShowMessage();
        }

        public void Unhighlight()
        {
            _canvas.TweenAlpha(1f, 0.3f, 1f, Ease.Linear);
            _blur.HideMessage();
        }
    }
}