using System.Collections.Generic;
using CodeBase.Features.Calls;
using CodeBase.Features.Calls.Infrastructure;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using Nadsat.DialogueGraph.Runtime;
using Reflex.Attributes;
using UnityEngine;

namespace CodeBase.Features.Cheats
{
    public class DebugSceneBootstrapper : MonoBehaviour
    {
        private CallExecutor _callExecutor;
        private CallRepository _callRepository;
        private DialogueGraphAdapter _adapter;
        private NodeRepository _nodes;

        [Inject]
        public void Construct(
            CallExecutor executor, 
            CallRepository repository, 
            DialogueGraphAdapter adapter,
            NodeRepository nodeRepository)
        {
            _callExecutor = executor;
            _callRepository = repository;
            _adapter = adapter;
            _nodes = nodeRepository;
        }

        public async void Setup(DialogueGraph graph, List<TextAsset> localization)
        {
            Debug.Log($"Start {graph.Name}");
            var dialogue = _adapter.GetDialogueFrom(graph, graph.Name, localization);
            _callRepository.Add(graph.Name, dialogue);
            await _callExecutor.Execute(graph.Name, destroyCancellationToken);
        }
    }
}