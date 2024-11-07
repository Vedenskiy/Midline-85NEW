using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.Features.Calls.Infrastructure.Extensions;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using Cysharp.Threading.Tasks;

namespace CodeBase.Features.Calls.Infrastructure
{
    public class NodeExecutor
    {
        private readonly Pipeline _pipeline;
        private readonly NodeScheduler _scheduler;

        private readonly Dictionary<string, (UniTask, Node)> _processing = new();
        private readonly List<string> _completed = new();

        public NodeExecutor(Pipeline pipeline, NodeScheduler scheduler)
        {
            _pipeline = pipeline;
            _scheduler = scheduler;
        }
        
        public async UniTask Execute(Node entry, CancellationToken token = default)
        {
            var task = _pipeline.Execute(entry, token);
            _processing.Add(entry.Guid, (task, entry));
            await ProcessWhileHasTasks(token);
        }

        private async Task ProcessWhileHasTasks(CancellationToken token)
        {
            while (_processing.Count > 0)
            {
                foreach (var (task, node) in _processing.Values)
                {
                    if (task.IsCompleted())
                        _completed.Add(node.Guid);
                }

                foreach (var completedNodeId in _completed)
                {
                    ProcessCompletedTask(completedNodeId, token);
                    _processing.Remove(completedNodeId);
                }

                _completed.Clear();

                await UniTask.Yield();
            }
        }

        private void ProcessCompletedTask(string completedNodeId, CancellationToken token)
        {
            var (task, node) = _processing[completedNodeId];
            StartNodeExecution(_scheduler.GetNextNodes(node), token);
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
                _processing.Add(node.Guid, (processingTask, node));
            }
        }
    }
}