using UnityEngine;

namespace FronkonGames.TinyTween.Tweens
{
    /// <summary> Tween Quaternion. </summary>
    public sealed class TweenQuaternion : Tween<Quaternion>
    {
        /// <summary> Create a Tween Quaternion and add it to the TinyTween manager. </summary>
        /// <returns>Tween.</returns>
        public static Tween<Quaternion> Create() => TinyTween.Instance.Add(new TweenQuaternion()) as Tween<Quaternion>;

        private static Quaternion Lerp(ITween<Quaternion> t, Quaternion start, Quaternion end, float progress, bool clamp) =>
            clamp == true ? Quaternion.Lerp(start, end, progress) : Quaternion.LerpUnclamped(start, end, progress);   

        private TweenQuaternion() : base(Lerp) { }
    }
}