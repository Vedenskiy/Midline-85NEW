using System.Collections.Generic;
using CodeBase.Features.Calls.Handlers.Branches;
using CodeBase.Features.Calls.Handlers.Choices;

namespace CodeBase.Features.Calls.Infrastructure.Nodes
{
    public class NodeScheduler
    {
        private readonly NodeRepository _nodes;
        private readonly PlayerChoices _playerChoices;
        private readonly BranchEvaluator _branchHandler;

        public NodeScheduler(NodeRepository nodes, PlayerChoices playerChoices, BranchEvaluator branchHandler)
        {
            _nodes = nodes;
            _playerChoices = playerChoices;
            _branchHandler = branchHandler;
        }
        
        public IEnumerable<Node> GetNextNodes(Node previous) => 
            GetFunctionalChildren(previous);

        /// <summary>
        /// Get functional nodes (ignore branches and nodes without logic)
        /// </summary>
        /// <param name="previous">Parent node</param>
        /// <returns>Collection of nodes for execution</returns>
        private IEnumerable<Node> GetFunctionalChildren(Node previous)
        {
            var previousId = previous is ChoicesData ? _playerChoices.LastChoiceId : previous.Guid;
            var children = _nodes.GetChildrenFrom(previousId);
            var resultChildren = FilterBranches(children);
            return resultChildren;
        }

        private IEnumerable<Node> FilterBranches(IEnumerable<Node> children)
        {
            var branches = new Queue<BranchesData>();
            var result = new List<Node>();

            AddBranchesToQueue(children, branches, result);

            while (branches.Count > 0)
            {
                var branch = branches.Dequeue();
                var next = _branchHandler.GetNextBranch(branch);
                var branchChildren = _nodes.GetChildrenFrom(next);
                AddBranchesToQueue(branchChildren, branches, result);
            }

            return result;
        }

        private static void AddBranchesToQueue(IEnumerable<Node> children, Queue<BranchesData> branches, ICollection<Node> result)
        {
            foreach (var child in children)
            {
                if (child is BranchesData data)
                    branches.Enqueue(data);
                else
                    result.Add(child);
            }
        }
    }
}