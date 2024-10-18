using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CodeBase.Features.Calls.Handlers.Choices;
using CodeBase.Features.Calls.Handlers.Phrases;
using CodeBase.Features.Calls.Handlers.Variables;
using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using CodeBase.Features.Calls.Infrastructure.Nodes.Branches;
using Cysharp.Threading.Tasks;
using Nadsat.DialogueGraph.Runtime;
using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private TextMeshProUGUI _endLevel;
        
        private CallsExecutor _executor;
        private NodeRepository _nodes;
        private DialogueLoader _loader;
        private DialogueGraphAdapter _adapter;
        
        [Inject]
        public void Construct(CallsExecutor executor, NodeRepository nodes, DialogueLoader loader, DialogueGraphAdapter adapter)
        {
            _executor = executor;
            _nodes = nodes;
            _loader = loader;
            _adapter = adapter;
        }

        private void OnEnable() => 
            _startButton.onClick.AddListener(StartGame);

        private void OnDisable() => 
            _startButton.onClick.RemoveListener(StartGame);

        private async void StartGame()
        {
            Debug.Log("START");
            await LoadCall("pizza");
        }

        private async UniTask LoadCall(string callName, CancellationToken token = default)
        {
            var locations = await Addressables.LoadResourceLocationsAsync(callName);
            DebugLocations(locations);

            var downloadSize = await Addressables.GetDownloadSizeAsync(locations);
            Debug.Log($"download size: {downloadSize}");

            var downloadHandle = Addressables.DownloadDependenciesAsync(locations);
            while (!downloadHandle.IsDone && downloadHandle.IsValid())
            {
                await UniTask.Delay(100, cancellationToken: token);
                Debug.Log($"Download percent: {downloadHandle.GetDownloadStatus().Percent}");
            }
            
            if (downloadHandle.Status == AsyncOperationStatus.Failed)
                Debug.LogError("Error while downloading catalog dependencies");
            
            if (downloadHandle.IsValid())
                Addressables.Release(downloadHandle);
            
            Debug.Log($"Call {callName} loaded!");
        }

        private void DebugLocations(IEnumerable<IResourceLocation> locations)
        {
            var builder = new StringBuilder();
            foreach (var location in locations) 
                builder.Append($"{location};");
            Debug.Log($"Locations: {builder}");
        }
        
        private IEnumerator Testing()
        {
            const string pizza = "Calls/Pizza/Pizza.asset";
            const string phrases = "Calls/Pizza/Phrases/";
            var downloadSize = Addressables.GetDownloadSizeAsync(pizza);
            yield return downloadSize;
            Debug.Log($"Size: {downloadSize.Result}");
            var value = Addressables.LoadAssetAsync<DialogueGraphContainer>(pizza);
            yield return value;
            Debug.Log($"{value.Result}");

            var locations = Addressables.LoadResourceLocationsAsync("pizza");
            yield return locations;

            foreach (var location in locations.Result)
            {
                Debug.Log($"Resource Location: {location}");
            }
            
            var phrasesLoading = Addressables.LoadAssetsAsync<Object>(locations.Result, (result) =>
            {
                Debug.Log($"Loaded: {result}");
            });

            yield return phrasesLoading;
        }

        private async UniTask LoadAndStartGame()
        {
            var dialogue = _adapter.Load("Pizza");
            _nodes.Load(dialogue.GetAllNodes(), dialogue.Links);
            await _executor.Execute(_nodes.GetById(dialogue.EntryNodeId), destroyCancellationToken);
            _endLevel.text = "Level Completed!";
        }

        private IEnumerable<NodeLink> GetTestLinks()
        {
            yield return Link("0", "1");
            yield return Link("1", "2");
            yield return Link("2", "3");
            yield return Link("3", "4");
            
            yield return Link("choice_1", "5");
            yield return Link("choice_2", "6");
            yield return Link("choice_3", "7");
            
            yield return Link("5", "8");
            yield return Link("6", "9");
            yield return Link("7", "10");
            yield return Link("branch_1", "11");
            yield return Link("branch_2", "12");
        }

        private IEnumerable<Node> GetTestPhrases()
        {
            yield return new VariableNode() { Guid = "0", VariableName = "test", Value = -1 };
            //yield return ElenaSay("Hello, World!", "0");
            yield return MarkSay("Hello, Elena!", "1");
            yield return ElenaSay("How are you?", "2");
            yield return MarkSay("I'm fine, how you?", "3");
            yield return new ChoicesNode()
            {
                Guid = "4",
                Choices = new List<ChoiceData>()
                {
                    new ChoiceData() { ChoiceId = "choice_1"},
                    new ChoiceData() { ChoiceId = "choice_2"},
                    new ChoiceData() { ChoiceId = "choice_3"},
                }
            };

            yield return new BranchesNode()
            {
                Guid = "5",
                Branches = new List<Branch>()
                {
                    new Branch() { Condition = "test > 0", Guid = "branch_1" },
                    new Branch() { Condition = "test < 0", Guid = "branch_2" },
                }
            };
            
            //yield return ElenaSay("I'm, CHOICE 1!", "5");
            yield return ElenaSay("I'm, CHOICE 2!", "6");
            yield return ElenaSay("I'm, CHOICE 3!", "7");
            
            yield return MarkSay("Wow, choice 1!", "8");
            yield return MarkSay("Wow, choice 2!", "9");
            yield return MarkSay("Wow, choice 3!", "10");
            
            yield return MarkSay("Wow, test variable more!", "11");
            yield return MarkSay("Wow, where test variable?!", "12");
        }

        private NodeLink Link(string parent, string child) => 
            new() { ParentId = parent, ChildId = child };

        private Node ElenaSay(string message, string withId) => 
            new PhraseNode() { Guid = withId, PersonKey = "Elena", MessageKey = message, DurationInSeconds = 2f };

        private Node MarkSay(string message, string withId) =>
            new PhraseNode() { Guid = withId, PersonKey = "Mark", MessageKey = message, DurationInSeconds = 2f };
    }
}