using TMPro;
using UnityEngine;

namespace CodeBase.Infrastructure.Common.BuildVersion
{
    public class BuildVersionLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;

        private void Start()
        {
            _label.text = $"Build version: {Application.version}";
        }
    }
}
