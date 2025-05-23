using ChocDino.UIFX;
using FronkonGames.TinyTween.Easing;
using FronkonGames.TinyTween.Tweens;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases.UI
{
    public class CallPhraseBlur : MonoBehaviour
    {
        private const float MinimumBlurStrength = 0f;
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
                .OnEnd(value =>
                {
                    // The plugin UIFX has a trouble with blurring,
                    // when the strength becomes 0 and or
                    // change from value to 0 has too small a step - the text breaks.
                    // Issue: https://github.com/Chocolate-Dinosaur/UIFX/issues/21
                    
                    // The following code is needed to fix this situation:
                    _blur.Strength = origin;
                    _blur.Strength = destination;
                })
                .Start();
    }
}