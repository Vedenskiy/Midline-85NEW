using System;
using UnityEngine;

namespace CodeBase.Features.Calls.Configs
{
    [Serializable]
    public class LevelConfig 
    {
        public string Name;
        public string DownloadLabel;
        public Sprite Icon;
        public string Description;
        public CallConfig Test;
    }
}