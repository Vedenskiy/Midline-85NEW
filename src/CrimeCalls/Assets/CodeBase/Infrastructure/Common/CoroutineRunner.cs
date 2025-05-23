using UnityEngine;

namespace CodeBase.Infrastructure.Common
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void Start() => 
            DontDestroyOnLoad(gameObject);
    }
}