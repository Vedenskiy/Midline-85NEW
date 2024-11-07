using UnityEngine;

namespace FronkonGames.TinyTween
{
    /// <summary> Extensions to make it easy to use TinyTween. </summary>
    public static class TweenExtensions
    {
        /// <summary> Create and execute a tween. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start value.</param>
        /// <param name="destination">End value.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<float> Tween(this float self, float origin, float destination, float duration, Ease ease) =>
            TweenFloat.Create()
                .Origin(origin)
                .Destination(destination)
                .Duration(duration)
                .OnUpdate(tween => self = tween.Value)
                .Owner(self)
                .Easing(ease)
                .Start();

        /// <summary> Create and execute a tween, using as initial value the current value. </summary>
        /// <param name="self">Self.</param>
        /// <param name="destination">End value.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<float> Tween(this float self, float destination, float duration, Ease ease) => Tween(self, self, destination, duration, ease);

        /// <summary> Create and execute a tween. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start value.</param>
        /// <param name="destination">End value.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Vector3> Tween(this Vector3 self, Vector3 origin, Vector3 destination, float duration, Ease ease) =>
            TweenVector3.Create()
                .Origin(origin)
                .Destination(destination)
                .Duration(duration)
                .OnUpdate(tween => self = tween.Value)
                .Owner(self)
                .Easing(ease)
                .Start();
    
        /// <summary> Create and execute a tween, using as initial value the current value. </summary>
        /// <param name="self">Self.</param>
        /// <param name="destination">End value.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Vector3> Tween(this Vector3 self, Vector3 destination, float duration, Ease ease) => Tween(self, self, destination, duration, ease);

        /// <summary> Create and execute a tween. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start value.</param>
        /// <param name="destination">End value.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Quaternion> Tween(this Quaternion self, Quaternion origin, Quaternion destination, float duration, Ease ease) =>
            TweenQuaternion.Create()
                .Origin(origin)
                .Destination(destination)
                .Duration(duration)
                .OnUpdate(tween => self = tween.Value)
                .Owner(self)
                .Easing(ease)
                .Start();
    
        /// <summary> Create and execute a tween, using as initial value the current value. </summary>
        /// <param name="self">Self.</param>
        /// <param name="destination">End value.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Quaternion> Tween(this Quaternion self, Quaternion destination, float duration, Ease ease) => Tween(self, self, destination, duration, ease);
    
        /// <summary> Create and execute a tween. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start value.</param>
        /// <param name="destination">End value.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Color> Tween(this Color self, Color origin, Color destination, float duration, Ease ease) =>
            FronkonGames.TinyTween.TweenColor.Create()
                .Origin(origin)
                .Destination(destination)
                .Duration(duration)
                .OnUpdate(tween => self = tween.Value)
                .Owner(self)
                .Easing(ease)
                .Start();
    
        /// <summary> Create and execute a tween, using as initial value the current value. </summary>
        /// <param name="self">Self.</param>
        /// <param name="destination">End value.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Color> Tween(this Color self, Color destination, float duration, Ease ease) => Tween(self, self, destination, duration, ease);

        /// <summary> Moves a Transform. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start position.</param>
        /// <param name="destination">End position.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Vector3> TweenMove(this Transform self, Vector3 origin, Vector3 destination, float duration, Ease ease) =>
            TweenVector3.Create()
                .Origin(origin)
                .Destination(destination)
                .Duration(duration)
                .OnUpdate(tween => self.position = tween.Value)
                .Owner(self)
                .Easing(ease)
                .Start();

        /// <summary> Moves a Transform, using its current position as origin. </summary>
        /// <param name="self">Self.</param>
        /// <param name="destination">End position.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Vector3> TweenMove(this Transform self, Vector3 destination, float duration, Ease ease) => TweenMove(self, self.position, destination, duration, ease);

        /// <summary> Scale a Transform. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start scale.</param>
        /// <param name="destination">End scale.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Vector3> TweenScale(this Transform self, Vector3 origin, Vector3 destination, float duration, Ease ease) =>
            TweenVector3.Create()
                .Origin(origin)
                .Destination(destination)
                .Duration(duration)
                .OnUpdate(tween => self.localScale = tween.Value)
                .Owner(self)
                .Easing(ease)
                .Start();

        /// <summary> Scale a Transform, using its current scale as origin. </summary>
        /// <param name="self">Self.</param>
        /// <param name="destination">End scale.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Vector3> TweenScale(this Transform self, Vector3 destination, float duration, Ease ease) => TweenScale(self, self.localScale, destination, duration, ease);

        /// <summary> Rotate a Transform. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start rotation.</param>
        /// <param name="destination">End rotation.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Quaternion> TweenRotation(this Transform self, Quaternion origin, Quaternion destination, float duration, Ease ease) =>
            TweenQuaternion.Create()
                .Origin(origin)
                .Destination(destination)
                .Duration(duration)
                .OnUpdate(tween => self.rotation = tween.Value)
                .Owner(self)
                .Easing(ease)
                .Start();

        /// <summary> Rotate a Transform, using its current rotation as origin. </summary>
        /// <param name="self">Self.</param>
        /// <param name="destination">End rotation.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Quaternion> TweenRotation(this Transform self, Quaternion destination, float duration, Ease ease) => TweenRotation(self, self.rotation, destination, duration, ease);
    
        /// <summary> Moves a GameObject. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start position.</param>
        /// <param name="destination">End position.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Vector3> TweenMove(this GameObject self, Vector3 origin, Vector3 destination, float duration, Ease ease) => TweenMove(self.transform, origin, destination, duration, ease);
    
        /// <summary> Moves a GameObject, using its current position as origin. </summary>
        /// <param name="self">Self.</param>
        /// <param name="destination">End position.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Vector3> TweenMove(this GameObject self, Vector3 destination, float duration, Ease ease) => TweenMove(self.transform, self.transform.position, destination, duration, ease);
    
        /// <summary> Scale a GameObject. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start scale.</param>
        /// <param name="destination">End scale.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Vector3> TweenScale(this GameObject self, Vector3 origin, Vector3 destination, float duration, Ease ease) => TweenScale(self.transform, origin, destination, duration, ease);

        /// <summary> Scale a GameObject, using its current scale as origin. </summary>
        /// <param name="self">Self.</param>
        /// <param name="destination">End scale.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Vector3> TweenScale(this GameObject self, Vector3 destination, float duration, Ease ease) => TweenScale(self.transform, self.transform.localScale, destination, duration, ease);
    
        /// <summary> Rotate a GameObject. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start rotation.</param>
        /// <param name="destination">End rotation.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Quaternion> TweenRotation(this GameObject self, Quaternion origin, Quaternion destination, float duration, Ease ease) => TweenRotation(self.transform, origin, destination, duration, ease);

        /// <summary> Rotate a GameObject, using its current rotation as origin. </summary>
        /// <param name="self">Self.</param>
        /// <param name="destination">End rotation.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <returns>Tween.</returns>
        public static Tween<Quaternion> TweenRotation(this GameObject self, Quaternion destination, float duration, Ease ease) => TweenRotation(self.transform, self.transform.rotation, destination, duration, ease);
    
        /// <summary> Change the color of a Material. </summary>
        /// <param name="self">Self.</param>
        /// <param name="origin">Start color.</param>
        /// <param name="destination">End color.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <param name="name">The name of the color variable.</param>
        /// <returns>Tween.</returns>
        public static Tween<Color> TweenColor(this Material self, Color origin, Color destination, float duration, Ease ease, string name = "_Color") =>
            FronkonGames.TinyTween.TweenColor.Create()
                .Origin(origin)
                .Destination(destination)
                .Duration(duration)
                .OnUpdate(tween => self.SetColor(name, tween.Value))
                .Owner(self)
                .Easing(ease)
                .Start();
    
        /// <summary> Change the color of a Material, using its current color as origin. </summary>
        /// <param name="self">Self.</param>
        /// <param name="destination">End color.</param>
        /// <param name="duration">Time in seconds.</param>
        /// <param name="ease">Easing.</param>
        /// <param name="name">The name of the color variable.</param>
        /// <returns>Tween.</returns>
        public static Tween<Color> TweenColor(this Material self, Color destination, float duration, Ease ease, string name = "_Color") => TweenColor(self, self.GetColor(name), destination, duration, ease, name);
    }
}