using UnityEngine;

namespace FronkonGames.TinyTween
{
    public static class CanvasGroupExtensions
    {
        /// <summary> Tween canvas group alpha. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start alpha.</param>
        /// <param name="destination">End alpha.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<float> TweenAlpha(this CanvasGroup self, float origin, float destination, float duration, Ease ease) =>
            TweenFloat.Create()
                .Origin(origin)
                .Destination(destination)
                .Duration(duration)
                .OnUpdate(tween => self.alpha = tween.Value)
                .Owner(self)
                .Easing(ease)
                .Start();
    }
}