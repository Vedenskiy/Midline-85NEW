using System.Collections.Generic;
using CodeBase.Features.Calls.Handlers.Choices;

namespace CodeBase.Features.Calls.Infrastructure.Nodes
{
    public class NodeScheduler
    {
        private readonly NodeRepository _nodes;
        private readonly PlayerChoices _playerChoices;

        public NodeScheduler(NodeRepository nodes, PlayerChoices playerChoices)
        {
            _nodes = nodes;
            _playerChoices = playerChoices;
        }
        
        public IEnumerable<Node> GetNextNodes(Node previous)
        {
            if (previous is ChoicesData)
                return _nodes.GetChildrenFrom(_playerChoices.LastChoiceId);

            return _nodes.GetChildrenFrom(previous.Guid);
        }
    }
}