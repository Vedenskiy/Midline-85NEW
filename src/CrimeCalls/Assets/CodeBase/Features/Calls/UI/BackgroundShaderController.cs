using System;
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
        
        public bool IsShown { get; private set; }

        public void Show(Action onCompleted = null) => 
            Execute(Showing(onCompleted));

        public void Hide(Action onCompleted = null) => 
            Execute(Hiding(onCompleted));

        private void OnDestroy() => 
            _material.SetFloat(Progress, 0f);

        private void Execute(IEnumerator coroutine)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(coroutine);
        }

        private IEnumerator Showing(Action onCompleted = null)
        {
            IsShown = true;
            _material.SetFloat(IsDissolve, 0f);
            yield return SmoothMaterialProgressChange(0f, 1f, _showingDuration);
            onCompleted?.Invoke();
        }
        
        private IEnumerator Hiding(Action onCompleted = null)
        {
            IsShown = false;
            _material.SetFloat(IsDissolve, 1f);
            yield return SmoothMaterialProgressChange(1f, 0f, _hidingDuration);
            onCompleted?.Invoke();
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