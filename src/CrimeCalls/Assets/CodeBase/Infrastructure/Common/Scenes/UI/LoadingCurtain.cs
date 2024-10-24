using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.Common.Scenes.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvas;
        [SerializeField] private float _hideDuration = 0.5f;

        private void Start() => 
            DontDestroyOnLoad(this);

        public void Show()
        {
            gameObject.SetActive(true);
            _canvas.alpha = 1f;
        }

        public void Hide() => 
            StartCoroutine(Hiding());

        private IEnumerator Hiding()
        {
            var start = _canvas.alpha;
            var time = 0f;
            while (time < _hideDuration)
            {
                time += Time.deltaTime;
                _canvas.alpha = Mathf.Lerp(start, 0, time / _hideDuration);
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}