using UnityEngine;

namespace FronkonGames.TinyTween.Easing
{
    /// <summary> Easing functions. </summary>
    internal static class EasingFunctions
    {
        public static float Calculate(Ease ease, bool easingIn, bool easingOut, float t) => ease switch
        {
            Ease.Linear     => t,
            Ease.Sine       => easingIn && easingOut ? SineInOut(t)    : easingIn ? SineIn(t)    : SineOut(t),
            Ease.Quad       => easingIn && easingOut ? QuadInOut(t)    : easingIn ? QuadIn(t)    : QuadOut(t),
            Ease.Cubic      => easingIn && easingOut ? CubicInOut(t)   : easingIn ? CubicIn(t)   : CubicOut(t),
            Ease.Quart      => easingIn && easingOut ? QuartInOut(t)   : easingIn ? QuartIn(t)   : QuartOut(t),
            Ease.Quint      => easingIn && easingOut ? QuintInOut(t)   : easingIn ? QuintIn(t)   : QuintOut(t),
            Ease.Expo       => easingIn && easingOut ? ExpoInOut(t)    : easingIn ? ExpoIn(t)    : ExpoOut(t),
            Ease.Circ       => easingIn && easingOut ? CircInOut(t)    : easingIn ? CircIn(t)    : CircOut(t),
            Ease.Back       => easingIn && easingOut ? BackInOut(t)    : easingIn ? BackIn(t)    : BackOut(t),
            Ease.Elastic    => easingIn && easingOut ? ElasticInOut(t) : easingIn ? ElasticIn(t) : ElasticOut(t),
            Ease.Bounce     => easingIn && easingOut ? BounceInOut(t)  : easingIn ? BounceIn(t)  : BounceOut(t),
            _ => t
        };

        private static float SineIn(float t)    => 1.0f - Mathf.Cos(t * Mathf.PI / 2.0f);
        private static float SineOut(float t)   => Mathf.Sin(t * Mathf.PI / 2.0f);
        private static float SineInOut(float t) => 0.5f * (1.0f - Mathf.Cos(Mathf.PI * t));
    
        private static float QuadIn(float t)    => t * t;
        private static float QuadOut(float t)   => 1.0f - (1.0f - t) * (1.0f - t);
        private static float QuadInOut(float t) => t < 0.5f ? 2.0f * t * t : 1.0f - Mathf.Pow(-2.0f * t + 2.0f, 2.0f) / 2.0f;

        private static float CubicIn(float t) => t * t * t;
        private static float CubicOut(float t) => --t * t * t + 1.0f;
        private static float CubicInOut(float t) => t < 0.5f ? 4.0f * t * t * t : 1.0f - Mathf.Pow(-2.0f * t + 2.0f, 3.0f) / 2.0f;
    
        private static float QuartIn(float t) => t * t * t * t;
        private static float QuartOut(float t) => 1.0f - (--t * t * t * t);
        private static float QuartInOut(float t) => (t *= 2.0f) < 1.0f ? 0.5f * t * t * t * t : -0.5f * ((t -= 2.0f) * t * t * t - 2.0f);

        private static float QuintIn(float t) => t * t * t * t * t;
        private static float QuintOut(float t) => --t * t * t * t * t + 1.0f;
        private static float QuintInOut(float t) => t < 0.5f ? 16.0f * t * t * t * t * t : 1.0f - Mathf.Pow(-2.0f * t + 2.0f, 5.0f) / 2.0f;

        private static float ExpoIn(float t) => t == 0.0f ? 0.0f : Mathf.Pow(2.0f, 10.0f * t - 10.0f);
        private static float ExpoOut(float t) => t == 1.0f ? 1.0f : 1.0f - Mathf.Pow(2.0f, -10.0f * t);
        private static float ExpoInOut(float t) => t == 0.0f ? 0.0f
            : t == 1.0f ? 1.0f
            : t < 0.5f ? Mathf.Pow(2.0f, 20.0f * t - 10.0f) / 2.0f
            : (2.0f - Mathf.Pow(2.0f, -20.0f * t + 10.0f)) / 2.0f;

        private static float CircIn(float t) => 1.0f - Mathf.Sqrt(1.0f - Mathf.Pow(t, 2.0f));
        private static float CircOut(float t) => Mathf.Sqrt(1.0f - Mathf.Pow(t - 1.0f, 2.0f));
        private static float CircInOut(float t) => t < 0.5f ? (1.0f - Mathf.Sqrt(1.0f - Mathf.Pow(2.0f * t, 2.0f))) / 2.0f
            : (Mathf.Sqrt(1.0f - Mathf.Pow(-2.0f * t + 2.0f, 2.0f)) + 1.0f) / 2.0f;

        private static float BackIn(float t) => C3 * t * t * t - C1 * t * t;
        private static float BackOut(float t) => 1.0f + C3 * Mathf.Pow(t - 1.0f, 3.0f) + C1 * Mathf.Pow(t - 1.0f, 2.0f);
        private static float BackInOut(float t) => t < 0.5f ? Mathf.Pow(2.0f * t, 2.0f) * ((C2 + 1.0f) * 2.0f * t - C2) / 2.0f
            : (Mathf.Pow(2.0f * t - 2.0f, 2.0f) * ((C2 + 1.0f) * (t * 2.0f - 2.0f) + C2) + 2.0f) / 2.0f;

        private static float ElasticIn(float t) => -Mathf.Pow(2.0f, 10.0f * t - 10.0f) * Mathf.Sin((t * 10.0f - 10.75f) * C4);
        private static float ElasticOut(float t) => Mathf.Pow(2.0f, -10.0f * t) * Mathf.Sin((t * 10.0f - 0.75f) * C4) + 1.0f;      
        private static float ElasticInOut(float t) => t < 0.5f ? -(Mathf.Pow(2.0f, 20.0f * t - 10.0f) * Mathf.Sin((20.0f * t - 11.125f) * C5)) / 2f
            : Mathf.Pow(2.0f, -20.0f * t + 10.0f) * Mathf.Sin((20.0f * t - 11.125f) * C5) / 2.0f + 1.0f;

        private static float BounceIn(float t) => 1.0f - BounceOut(1.0f - t);
        private static float BounceOut(float t)
        {
            if (t < 1.0f / 2.75f)
                return 7.5625f * t * t;
      
            if (t < 2.0f / 2.75f)
                return 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f;
      
            if (t < 2.5f / 2.75f)
                return 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f;
      
            return 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f;
        }
        private static float BounceInOut(float t) => t < 0.5f ? BounceIn(t * 2.0f) * 0.5f : BounceOut(t * 2.0f - 1.0f) * 0.5f + 0.5f;

        private const float C1 = 1.70158f;
        private const float C2 = C1 * 1.525f;
        private const float C3 = C1 + 1.0f;
        private const float C4 = 2.0f * Mathf.PI / 3.0f;
        private const float C5 = 2.0f * Mathf.PI / 4.5f;
    }
}