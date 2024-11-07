using UnityEngine;

namespace FronkonGames.TinyTween.Tweens
{
    /// <summary> Tween float. </summary>
    public sealed class TweenFloat : Tween<float>
    {
        /// <summary> Create a Tween float and add it to the TinyTween manager. </summary>
        /// <returns>Tween.</returns>
        public static Tween<float> Create() => TinyTween.Instance.Add(new TweenFloat()) as Tween<float>;
    
        private static float Lerp(ITween<float> t, float start, float end, float progress, bool clamp) =>
            clamp == true ? Mathf.Lerp(start, end, progress) : Mathf.LerpUnclamped(start, end, progress);

        private TweenFloat() : base(Lerp) { }
    }
}