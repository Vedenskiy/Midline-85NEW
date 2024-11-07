using System;
using CodeBase.Features.Calls;
using CodeBase.Features.Calls.Configs;
using CodeBase.Infrastructure.Common.AssetManagement;
using CodeBase.Infrastructure.Common.AssetManagement.Reports;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace CodeBase.Features.Menu
{
    public class LevelDetailsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _icon;
        
        [SerializeField] private Button _downloadButton;
        [SerializeField] private LoadingProgressBar _downloadProgressBar;
        
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _returnButton;

        private LevelDownloadService _downloadService;
        private CallExecutor _callExecutor;
        private AssetDownloadReporterRegistry _reporters;

        private LevelConfig _config;
        private AssetDownloadReporter _reporter;
        
        public event Action Returned;

        [Inject]
        public void Construct(
            LevelDownloadService downloadService, 
            CallExecutor callExecutor,
            AssetDownloadReporterRegistry downloadReporterRegistry)
        {
            _downloadService = downloadService;
            _callExecutor = callExecutor;
            _reporters = downloadReporterRegistry;
        }

        public void Setup(LevelConfig config)
        {
            _config = config;
            _title.text = config.Name;
            _description.text = config.Description;
            _icon.sprite = config.Icon;
            _reporter = _reporters.GetOrCreate(_config.DownloadLabel);
        }

        private void OnEnable()
        {
            _downloadButton.onClick.AddListener(OnDownloadPressed);
            _startButton.onClick.AddListener(OnStartPressed);
            _returnButton.onClick.AddListener(OnReturnPressed);
            _reporter.Updated += OnReporterUpdated;
        }
        
        private void OnDisable()
        {
            _downloadButton.onClick.RemoveListener(OnDownloadPressed);
            _startButton.onClick.RemoveListener(OnStartPressed);
            _returnButton.onClick.RemoveListener(OnReturnPressed);
            _reporter.Updated -= OnReporterUpdated;
        }

        private void OnReporterUpdated(AssetDownloadReporter reporter)
        {
            var progress = reporter.Progress;
            _downloadProgressBar.Apply(progress);

            var targetSize = reporter.GetDownloadSizeMb();
            var loadedSize = progress * targetSize;
                
            var progressText = $"({loadedSize:F1} МБ / {targetSize:F1} МБ)";
            _downloadProgressBar.UpdateText($"Загрузка {progressText}");
        }
        
        private async void OnDownloadPressed()
        {
            _downloadButton.gameObject.SetActive(false);
            _downloadProgressBar.gameObject.SetActive(true);

            await _downloadService.LoadDialogue(_config.DownloadLabel, _reporter, destroyCancellationToken);
            
            _downloadProgressBar.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(true);
        }
        
        private async void OnStartPressed()
        {
            gameObject.SetActive(false);
            _callExecutor.Execute(_config.DownloadLabel).Forget();
        }
        
        private void OnReturnPressed() => 
            Returned?.Invoke();
    }
}
