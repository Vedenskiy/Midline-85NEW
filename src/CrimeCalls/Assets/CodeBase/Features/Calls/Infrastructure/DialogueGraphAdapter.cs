using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeBase.Features.Calls.Handlers.Choices;
using CodeBase.Features.Calls.Handlers.Images;
using CodeBase.Features.Calls.Handlers.Phrases;
using CodeBase.Features.Calls.Infrastructure.Nodes;
using CodeBase.Infrastructure.Common.AssetManagement;
using CodeBase.Infrastructure.Common.Localization;
using Nadsat.DialogueGraph.Runtime;
using Nadsat.DialogueGraph.Runtime.Nodes;
using UnityEngine;
using ChoicesNode = CodeBase.Features.Calls.Handlers.Choices.ChoicesNode;

namespace CodeBase.Features.Calls.Infrastructure
{
    public class DialogueGraphAdapter
    {
        private const string PathToLocalDialogues = "Dialogues";

        private readonly AssetProvider _assets;
        private readonly LocalizationService _localization;

        public DialogueGraphAdapter(AssetProvider assets, LocalizationService localization)
        {
            _assets = assets;
            _localization = localization;
        }
        
        public Dialogue GetDialogueFrom(DialogueGraphContainer graph, string levelName, List<TextAsset> localization)
        {
            InitializeLocalization(localization);
            return ConvertToDialogue(levelName, graph.Graph);
        }

        private void InitializeLocalization(IEnumerable<TextAsset> tables)
        {
            var mapping = new Dictionary<string, string>();
            foreach (var table in tables)
            {
                if (table.name == "pizza")
                    continue;
                
                var content = table.text;
                var linesOfText = content.Split('\n');
                foreach (var line in linesOfText)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    
                    var lineContent = line.Split(',', 2);
                    mapping[lineContent[0].Replace("\"", "")] = lineContent[1].Replace("\"", "");
                }
            }
            _localization.Load(mapping);
        }
        
        private Dialogue ConvertToDialogue(string levelName, DialogueGraph graph)
        {
            var dialogue = new Dialogue();

            dialogue.Nodes = ConvertNodes(graph).ToList();
            dialogue.Links = ConvertNodeLinks(levelName, graph.Links);
            dialogue.EntryNodeId = graph.EntryNodeGuid;

            return dialogue;
        }

        private IEnumerable<Node> ConvertNodes(DialogueGraph graph)
        {
            foreach (var phraseNode in ConvertDialogueToPhrases(graph.Nodes))
            {
                yield return phraseNode;
            }

            foreach (var choice in ConvertChoices(graph.ChoiceNodes))
            {
                yield return choice;
            }

            foreach (var emptyNode in ConvertEmptyNodes(graph.RedirectNodes))
            {
                yield return emptyNode;
            }

            foreach (var image in ConvertImageNodes(graph.Images))
            {
                yield return image;
            }
        }

        private IEnumerable<ImageNode> ConvertImageNodes(List<BackgroundImageNode> images) =>
            images.Select(image => new ImageNode()
            {
                Guid = image.Guid,
                PathToImage = image.PathToImage,
                IgnoreDuration = true
            });

        private static List<NodeLink> ConvertNodeLinks(string levelName, List<NodeLinks> links) =>
            links.Select(link => new NodeLink()
                {
                    ParentId = link.FromPortId.ToLower().StartsWith(levelName) ? link.FromPortId : link.FromGuid,
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

        private List<PhraseNode> ConvertDialogueToPhrases(IEnumerable<DialogueNode> nodes) =>
            nodes.Select(node => new PhraseNode()
                {
                    Guid = node.Guid, 
                    PersonKey = node.PersonId, 
                    MessageKey = node.PhraseId, 
                    DurationInSeconds = CalculateTimeForPhrase(node.PhraseId),
                })
                .ToList();

        private float CalculateTimeForPhrase(string phraseId)
        {
            var delimiters = new[] { ' ', '\r', '\n' };
            var text = _localization.GetTranslatedString(phraseId);
            var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return words.Length * 0.4f;
        }
    }
}