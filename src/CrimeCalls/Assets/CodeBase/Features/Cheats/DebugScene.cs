using UnityEngine;

namespace CodeBase.Features.Cheats
{
    public class DebugScene : MonoBehaviour
    {
        public static int Value;

        private void Start()
        {
            Debug.Log($"Value = {Value}");
        }
    }
}