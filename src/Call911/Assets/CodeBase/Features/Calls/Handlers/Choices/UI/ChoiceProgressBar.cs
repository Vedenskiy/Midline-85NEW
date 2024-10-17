using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Features.Calls.Handlers.Choices.UI
{
    public class ChoiceProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _left;
        [SerializeField] private Image _right;

        public float Progress { get; private set; }
        
        /// <summary>
        /// Apply current value on progress bar.
        /// </summary>
        /// <param name="progress">Value between 0 and 1.</param>
        public void SetProgress(float progress)
        {
            Progress = progress;
            var targetScale = new Vector3(1 - progress, 1, 1);
            _left.transform.localScale = targetScale;
            _right.transform.localScale = targetScale;
        }
    }
}