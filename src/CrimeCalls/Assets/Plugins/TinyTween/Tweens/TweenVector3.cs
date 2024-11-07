using UnityEngine;

namespace FronkonGames.TinyTween.Tweens
{
    /// <summary> Tween Vector3. </summary>
    public sealed class TweenVector3 : Tween<Vector3>
    {
        /// <summary> Create a Tween Vector3 and add it to the TinyTween manager. </summary>
        /// <returns>Tween.</returns>
        public static Tween<Vector3> Create() => TinyTween.Instance.Add(new TweenVector3()) as Tween<Vector3>;
    
        private static Vector3 Lerp(ITween<Vector3> t, Vector3 start, Vector3 end, float progress, bool clamp) =>
            clamp == true ? Vector3.Lerp(start, end, progress) : Vector3.LerpUnclamped(start, end, progress);

        private TweenVector3() : base(Lerp) { }
    }
}