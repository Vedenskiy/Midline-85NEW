using System;
using System.Collections.Generic;
using CodeBase.Features.Calls.Handlers.Choices;
using CodeBase.Features.Calls.Handlers.Phrases;
using CodeBase.Features.Calls.Handlers.Variables;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using CodeBase.Features.Calls.Infrastructure.Nodes.Branches;
using NUnit.Framework;

namespace CodeBase.Features.Calls.Infrastructure
{
    [Serializable]
    public class Dialogue
    {
        public string EntryNodeId;
        public List<Node> Nodes = new();
        public List<NodeLink> Links;

        public IEnumerable<Node> GetAllNodes() => 
            Nodes;
    }
}