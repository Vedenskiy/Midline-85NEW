using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeBase.Features.Calls.Handlers.Choices;
using CodeBase.Features.Calls.Handlers.Phrases;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using CodeBase.Features.Calls.Infrastructure.Nodes.Branches;
using CodeBase.Infrastructure.Common.AssetManagement;
using Nadsat.DialogueGraph.Runtime;
using Nadsat.DialogueGraph.Runtime.Nodes;
using ChoicesNode = CodeBase.Features.Calls.Handlers.Choices.ChoicesNode;
using VariableNode = CodeBase.Features.Calls.Handlers.Variables.VariableNode;

namespace CodeBase.Features.Calls.Infrastructure
{
    public class DialogueGraphAdapter
    {
        private const string PathToLocalDialogues = "Dialogues/";

        private readonly AssetProvider _assets;

        public DialogueGraphAdapter(AssetProvider assets) => 
            _assets = assets;

        public Dialogue Load(string level)
        {
            var pathToLevel = Path.Combine(PathToLocalDialogues, level, level);
            var container = _assets.LoadResource<DialogueGraphContainer>(pathToLevel);
            return ConvertToDialogue(container.Graph);
        }

        private Dialogue ConvertToDialogue(DialogueGraph graph)
        {
            var dialogue = new Dialogue();

            dialogue.Phrases = ConvertDialogueToPhrases(graph.Nodes);
            dialogue.Choices = ConvertChoices(graph.ChoiceNodes);
            dialogue.Empties = ConvertEmptyNodes(graph.RedirectNodes);
            dialogue.Links = ConvertNodeLinks(graph.Links);
            dialogue.EntryNodeId = graph.EntryNodeGuid;

            dialogue.Variables = new List<VariableNode>();
            dialogue.Branches = new List<BranchesNode>();
            
            return dialogue;
        }

        private static List<NodeLink> ConvertNodeLinks(List<NodeLinks> links) =>
            links.Select(link => new NodeLink()
                {
                    ParentId = link.FromPortId.StartsWith("Pizza") ? link.FromPortId : link.FromGuid,
                    ChildId = link.ToGuid,
                })
                .ToList();

        private static List<Node> ConvertEmptyNodes(IEnumerable<RedirectNode> nodes) =>
            nodes.Select(node => new Node() 
                { 
                    Guid = node.Guid 
                })
                .ToList();

        private static List<ChoicesNode> ConvertChoices(IEnumerable<Nadsat.DialogueGraph.Runtime.Nodes.ChoicesNode> nodes) =>
            nodes.Select(node => new ChoicesNode()
                {
                    Guid = node.Guid,
                    Choices = node.Choices.Select(choice => new ChoiceData()
                    {
                        ChoiceId = choice.ChoiceId,
                        IsLocked = choice.IsLocked,
                        UnlockedCondition = choice.UnlockedCondition
                    }).ToList()
                })
                .ToList();

        private static List<PhraseNode> ConvertDialogueToPhrases(IEnumerable<DialogueNode> nodes) =>
            nodes.Select(node => new PhraseNode()
                {
                    Guid = node.Guid, 
                    PersonKey = node.PersonId, 
                    MessageKey = node.PhraseId, 
                    DurationInSeconds = 4,
                })
                .ToList();
    }
}