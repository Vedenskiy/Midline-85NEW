using UnityEngine;

namespace FronkonGames.TinyTween
{
    /// <summary> Tween Vector4. </summary>
    public sealed class TweenVector4 : Tween<Vector4>
    {
        /// <summary> Create a Tween Vector4 and add it to the TinyTween manager. </summary>
        /// <returns>Tween.</returns>
        public static Tween<Vector4> Create() => TinyTween.Instance.Add(new TweenVector4()) as Tween<Vector4>;
    
        private static Vector4 Lerp(ITween<Vector4> t, Vector4 start, Vector4 end, float progress, bool clamp) =>
            clamp == true ? Vector4.Lerp(start, end, progress) : Vector4.LerpUnclamped(start, end, progress);

        private TweenVector4() : base(Lerp) { }
    }
}