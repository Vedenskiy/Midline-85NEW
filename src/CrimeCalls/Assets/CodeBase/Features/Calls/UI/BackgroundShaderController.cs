using System.Collections;
using UnityEngine;

namespace CodeBase.Features.Calls.UI
{
    public class BackgroundShaderController : MonoBehaviour
    {
        private static readonly int Progress = Shader.PropertyToID("_Progress");
        private static readonly int IsDissolve = Shader.PropertyToID("_IsDissolve");

        [SerializeField] private Material _material;
        
        [SerializeField] private float _showingDuration = 1f;
        [SerializeField] private float _hidingDuration = 1f;

        private Coroutine _coroutine;

        public void Show() => 
            Execute(Showing());

        public void Hide() => 
            Execute(Hiding());

        private void OnDestroy() => 
            _material.SetFloat(Progress, 0f);

        private void Execute(IEnumerator coroutine)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(coroutine);
        }

        private IEnumerator Showing()
        {
            _material.SetFloat(IsDissolve, 0f);
            yield return SmoothMaterialProgressChange(0f, 1f, _showingDuration);
        }
        
        private IEnumerator Hiding()
        {
            _material.SetFloat(IsDissolve, 1f);
            yield return SmoothMaterialProgressChange(1, 0, _hidingDuration);
        }

        private IEnumerator SmoothMaterialProgressChange(float origin, float target, float duration)
        {
            var time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime / duration;
                var progress = Mathf.Lerp(origin, target, time);
                _material.SetFloat(Progress, progress);
                yield return null;
            }
        }
    }
}