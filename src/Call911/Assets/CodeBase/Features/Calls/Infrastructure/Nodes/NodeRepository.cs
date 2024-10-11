using System.Collections.Generic;

namespace CodeBase.Features.Calls.Infrastructure.Nodes
{
    public class NodeRepository
    {
        public Node GetById(string guid)
        {
            return new Node();
        }

        public IEnumerable<Node> GetChildrenFrom(string guid)
        {
            yield return new Node();
        }
    }
}