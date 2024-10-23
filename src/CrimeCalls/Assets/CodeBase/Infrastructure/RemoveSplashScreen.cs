using UnityEngine;
using UnityEngine.Rendering;

namespace CodeBase.Infrastructure
{
    public class RemoveSplashScreen 
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void StopSplashScreen()
        {
            Debug.Log("Stop splash screen!");
            SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
        }
    }
}