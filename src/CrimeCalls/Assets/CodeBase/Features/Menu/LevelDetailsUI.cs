using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Features.Menu
{
    public class LevelDetailsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;
        
        [SerializeField] private Button _downloadButton;
        [SerializeField] private LoadingProgressBar _downloadProgressBar;
        
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _returnButton;
    }
}
