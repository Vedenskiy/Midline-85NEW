using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Features.Calls.Infrastructure.Nodes
{
    public class NodeRepository
    {
        private readonly Dictionary<string, Node> _nodes;
        private readonly Dictionary<string, List<string>> _links;

        public NodeRepository()
        {
            _nodes = new Dictionary<string, Node>();
            _links = new Dictionary<string, List<string>>();
        }
        
        public void Load(IEnumerable<Node> nodes, IEnumerable<NodeLink> links)
        {
            foreach (var node in nodes) 
                _nodes.Add(node.Guid, node);

            foreach (var link in links)
            {
                if (!_links.ContainsKey(link.ParentId))
                    _links[link.ParentId] = new List<string>();
                
                _links[link.ParentId].Add(link.ChildId);
            }
        }

        public Node GetById(string guid) => 
            _nodes[guid];

        public IEnumerable<Node> GetChildrenFrom(string guid) => 
            _links[guid].Select(childId => _nodes[childId]);
    }
}