using UnityEngine;

namespace FronkonGames.TinyTween.Tweens
{
    /// <summary> Tween Vector2. </summary>
    public sealed class TweenVector2 : Tween<Vector2>
    {
        /// <summary> Create a Tween Vector2 and add it to the TinyTween manager. </summary>
        /// <returns>Tween.</returns>
        public static Tween<Vector2> Create() => TinyTween.Instance.Add(new TweenVector2()) as Tween<Vector2>;

        private static Vector2 Lerp(ITween<Vector2> t, Vector2 start, Vector2 end, float progress, bool clamp) =>
            clamp == true ? Vector2.Lerp(start, end, progress) : Vector2.LerpUnclamped(start, end, progress); 

        private TweenVector2() : base(Lerp) { }
    }
}