using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using Cysharp.Threading.Tasks;
using UniTaskExtensions = CodeBase.Features.Calls.Infrastructure.Extensions.UniTaskExtensions;

namespace CodeBase.Features.Calls.Infrastructure
{
    public class CallsExecutor
    {
        private readonly Pipeline _pipeline;
        private readonly NodeScheduler _scheduler;

        private readonly Dictionary<UniTask, Node> _processingTasks = new();
        private readonly List<UniTask> _completedTasks = new();

        public CallsExecutor(Pipeline pipeline, NodeScheduler scheduler)
        {
            _pipeline = pipeline;
            _scheduler = scheduler;
        }
        
        public async UniTask Execute(Node entry, CancellationToken token = default)
        {
            _processingTasks.Add(_pipeline.Execute(entry, token), entry);
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
            StartNodeExecution(_scheduler.GetNextNodes(completedNode), token);
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