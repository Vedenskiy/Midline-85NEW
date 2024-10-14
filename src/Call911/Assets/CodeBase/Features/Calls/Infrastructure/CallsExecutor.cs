using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.Features.Calls.Handlers.Choices;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using Cysharp.Threading.Tasks;
using UniTaskExtensions = CodeBase.Features.Calls.Infrastructure.Extensions.UniTaskExtensions;

namespace CodeBase.Features.Calls.Infrastructure
{
    public class CallsExecutor
    {
        private readonly Pipeline _pipeline;
        private readonly NodeRepository _nodes;
        private readonly PlayerChoices _playerChoices;

        private readonly Dictionary<UniTask, Node> _processingTasks = new();
        private readonly List<UniTask> _completedTasks = new();

        public CallsExecutor(
            Pipeline pipeline, 
            NodeRepository nodes, 
            PlayerChoices playerChoices)
        {
            _pipeline = pipeline;
            _nodes = nodes;
            _playerChoices = playerChoices;
        }

        public async UniTask Execute(string entryGuid, CancellationToken token = default)
        {
            var entryNode = _nodes.GetById(entryGuid);
            _processingTasks.Add(_pipeline.Execute(entryNode, token), entryNode);
            await ProcessWhileHasTasks(token);
        }

        private async Task ProcessWhileHasTasks(CancellationToken token)
        {
            while (_processingTasks.Count > 0)
            {
                _completedTasks.AddRange(_processingTasks.Keys.Where(UniTaskExtensions.IsCompleted));

                foreach (var completedTask in _completedTasks)
                {
                    ProcessCompletedTask(completedTask, token);
                    _processingTasks.Remove(completedTask);
                }

                _completedTasks.Clear();

                await UniTask.Yield();
            }
        }

        private void ProcessCompletedTask(UniTask completedTask, CancellationToken token)
        {
            var completedNode = _processingTasks[completedTask];
            StartNodeExecution(GetNextNodes(completedNode), token);
        }

        private IEnumerable<Node> GetNextNodes(Node completedNode)
        {
            if (completedNode is ChoicesData)
                return _nodes.GetChildrenFrom(_playerChoices.LastChoiceId);

            return _nodes.GetChildrenFrom(completedNode.Guid);
        }

        private void StartNodeExecution(IEnumerable<Node> nodes, CancellationToken token)
        {
            if (nodes == null)
                return;

            foreach (var node in nodes)
            {
                if (node.IsProcessed)
                    continue;

                node.IsProcessed = true;
                var processingTask = _pipeline.Execute(node, token);
                _processingTasks.Add(processingTask, node);
            }
        }
    }
}