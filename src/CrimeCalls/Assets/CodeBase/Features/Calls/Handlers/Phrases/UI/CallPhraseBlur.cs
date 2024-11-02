using ChocDino.UIFX;
using FronkonGames.TinyTween;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases.UI
{
    public class CallPhraseBlur : MonoBehaviour
    {
        private const float MinimumBlurStrength = 0.05f;
        private const float MaximumBlurStrength = 1f;
        private const float ShowBlurDuration = 1f;
        private const float HideBlurDuration = 2f;
        
        [SerializeField] private BlurFilter _blur;

        private Tween<float> _tween;

        public void ShowMessage() => 
            _tween = SmoothBlurStrengthTween(MaximumBlurStrength, MinimumBlurStrength, ShowBlurDuration);

        public void HideMessage() => 
            _tween = SmoothBlurStrengthTween(MinimumBlurStrength, MaximumBlurStrength, HideBlurDuration);

        private Tween<float> SmoothBlurStrengthTween(float origin, float destination, float duration) =>
            TweenFloat.Create()
                .Origin(origin)
                .Destination(destination)
                .Duration(duration)
                .Easing(Ease.Linear)
                .OnUpdate(value => _blur.Strength = value.Value)
                .Start();
    }
}