using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using CodeBase.Infrastructure.Common.AssetManagement;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _endLevel;
        
        private CallsExecutor _executor;
        private NodeRepository _nodes;
        private LevelDownloadService _downloadService;

        [Inject]
        public void Construct(CallsExecutor executor, NodeRepository nodes, LevelDownloadService downloadService)
        {
            _executor = executor;
            _nodes = nodes;
            _downloadService = downloadService;
        }

        private void OnEnable() => 
            _startButton.onClick.AddListener(StartGame);

        private void OnDisable() => 
            _startButton.onClick.RemoveListener(StartGame);

        private async void StartGame()
        {
            Debug.Log("START");
            await _downloadService.LoadDialogue("pizza");
        }
        
        private async UniTask LoadAndStartGame(Dialogue dialogue)
        {
            _nodes.Load(dialogue.GetAllNodes(), dialogue.Links);
            await _executor.Execute(_nodes.GetById(dialogue.EntryNodeId), destroyCancellationToken);
            _endLevel.text = "Level Completed!";
        }
    }
}