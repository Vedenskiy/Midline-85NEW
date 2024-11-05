using System;
using CodeBase.Features.Calls.Configs;
using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Calls.Infrastructure.Nodes;
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

        private CallsExecutor _executor;
        private NodeRepository _nodes;
        private LevelDownloadService _downloadService;
        private AssetDownloadReporterRegistry _reporters;

        private Dialogue _dialogue;
        private LevelConfig _config;

        public event Action Returned;

        [Inject]
        public void Construct(
            CallsExecutor executor, 
            NodeRepository nodes, 
            LevelDownloadService downloadService, 
            AssetDownloadReporterRegistry downloadReporterRegistry)
        {
            _executor = executor;
            _nodes = nodes;
            _downloadService = downloadService;
            _reporters = downloadReporterRegistry;
        }

        public void Setup(LevelConfig config)
        {
            _config = config;
            _title.text = config.Name;
            _description.text = config.Description;
            _icon.sprite = config.Icon;
        }

        private void OnEnable()
        {
            _downloadButton.onClick.AddListener(OnDownloadPressed);
            _startButton.onClick.AddListener(OnStartPressed);
            _returnButton.onClick.AddListener(OnReturnPressed);

            _reporters[_config.DownloadLabel].Updated += OnReporterUpdated;
        }
        
        private void OnDisable()
        {
            _downloadButton.onClick.RemoveListener(OnDownloadPressed);
            _startButton.onClick.RemoveListener(OnStartPressed);
            _returnButton.onClick.RemoveListener(OnReturnPressed);
            
            _reporters[_config.DownloadLabel].Updated -= OnReporterUpdated;
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

            _dialogue = await _downloadService.LoadDialogue(_config.DownloadLabel, destroyCancellationToken);
            _downloadProgressBar.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(true);
        }
        
        private static float SizeToMb(long downloadSize) => downloadSize * 1f / 1048576;

        
        private async void OnStartPressed()
        {
            gameObject.SetActive(false);
            await StartGame(_dialogue);
        }
        
        private void OnReturnPressed() => 
            Returned?.Invoke();

        private async UniTask StartGame(Dialogue dialogue)
        {
            _nodes.Load(dialogue.GetAllNodes(), dialogue.Links);
            await _executor.Execute(_nodes.GetById(dialogue.EntryNodeId), destroyCancellationToken);
        }
    }
}
