using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using CodeBase.Infrastructure.Common.AssetManagement;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
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

        private CallsExecutor _executor;
        private NodeRepository _nodes;
        private LevelDownloadService _downloadService;

        private Dialogue _dialogue;

        [Inject]
        public void Construct(CallsExecutor executor, NodeRepository nodes, LevelDownloadService downloadService)
        {
            _executor = executor;
            _nodes = nodes;
            _downloadService = downloadService;
        }

        private void OnEnable()
        {
            _downloadButton.onClick.AddListener(OnDownloadPressed);
            _startButton.onClick.AddListener(OnStartPressed);
            _returnButton.onClick.AddListener(OnReturnPressed);
        }

        private void OnDisable()
        {
            _downloadButton.onClick.RemoveListener(OnDownloadPressed);
            _startButton.onClick.RemoveListener(OnStartPressed);
            _returnButton.onClick.RemoveListener(OnReturnPressed);
        }

        private async void OnDownloadPressed()
        {
            _downloadButton.gameObject.SetActive(false);
            _downloadProgressBar.gameObject.SetActive(true);
            
            _dialogue = await _downloadService.LoadDialogue("pizza", progress =>
            {
                _downloadProgressBar.Apply(progress);

                var targetSize = _downloadService.GetDownloadSizeMb();
                var loadedSize = progress * targetSize;
                
                var progressText = $"({loadedSize:F1} МБ / {targetSize:F1} МБ)";
                _downloadProgressBar.UpdateText($"Загрузка {progressText}");
            });
            
            _downloadProgressBar.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(true);
        }
        
        private async void OnStartPressed()
        {
            gameObject.SetActive(false);
            await StartGame(_dialogue);
        }
        
        private void OnReturnPressed()
        {
            Debug.Log("RETURN TO MENU!");
        }
        
        private async UniTask StartGame(Dialogue dialogue)
        {
            _nodes.Load(dialogue.GetAllNodes(), dialogue.Links);
            await _executor.Execute(_nodes.GetById(dialogue.EntryNodeId), destroyCancellationToken);
        }
    }
}
