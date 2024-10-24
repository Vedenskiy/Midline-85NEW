using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Features.Calls.Configs
{
    [CreateAssetMenu(fileName = "LevelsCatalog", menuName = "Configs/Levels", order = 0)]
    public class LevelsCatalog : ScriptableObject
    {
        public List<LevelConfig> Levels;
    }
}