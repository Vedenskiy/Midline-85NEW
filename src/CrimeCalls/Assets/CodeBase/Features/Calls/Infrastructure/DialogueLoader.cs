using CodeBase.Infrastructure.Common.AssetManagement;
using UnityEngine;

namespace CodeBase.Features.Calls.Infrastructure
{
    public class DialogueLoader
    {
        private const string PathToLocalDialogues = "Dialogues/";

        private readonly AssetProvider _assets;

        public DialogueLoader(AssetProvider assets) => 
            _assets = assets;

        public Dialogue Load(string levelName)
        {
            var jsonContent = _assets.LoadResource<TextAsset>($"{PathToLocalDialogues}{levelName}");
            return null;
        }
    }
}