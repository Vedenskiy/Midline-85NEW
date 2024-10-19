using System;
using System.Collections.Generic;
using CodeBase.Features.Calls.Handlers.Choices;
using CodeBase.Features.Calls.Handlers.Phrases;
using CodeBase.Features.Calls.Handlers.Variables;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using CodeBase.Features.Calls.Infrastructure.Nodes.Branches;

namespace CodeBase.Features.Calls.Infrastructure
{
    [Serializable]
    public class Dialogue
    {
        public string EntryNodeId;
        
        public List<PhraseNode> Phrases;
        public List<ChoicesNode> Choices;
        public List<VariableNode> Variables;
        public List<BranchesNode> Branches;

        public List<Node> Empties;

        public List<NodeLink> Links;

        public IEnumerable<Node> GetAllNodes()
        {
            var nodes = new List<Node>();
            nodes.AddRange(Phrases);
            nodes.AddRange(Choices);
            nodes.AddRange(Variables);
            nodes.AddRange(Branches);
            nodes.AddRange(Empties);
            return nodes;
        }
    }
}