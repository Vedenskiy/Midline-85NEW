using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Features.Menu
{
    public class LoadingProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _loadingText;
        
        public void Apply(float progress)
        {
            _slider.value = progress;
        }

        public void UpdateText(string text)
        {
            _loadingText.text = text;
        }
    }
}