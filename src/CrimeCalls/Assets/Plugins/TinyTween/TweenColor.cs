using UnityEngine;

namespace FronkonGames.TinyTween
{
    /// <summary> Tween Color. </summary>
    public sealed class TweenColor : Tween<Color>
    {
        /// <summary> Create a Tween Color and add it to the TinyTween manager. </summary>
        /// <returns>Tween.</returns>
        public static Tween<Color> Create() => TinyTween.Instance.Add(new TweenColor()) as Tween<Color>;

        private static Color Lerp(ITween<Color> t, Color start, Color end, float progress, bool clamp) =>
            clamp == true ? Color.Lerp(start, end, progress) : Color.LerpUnclamped(start, end, progress);

        private TweenColor() : base(Lerp) { }
    }
}