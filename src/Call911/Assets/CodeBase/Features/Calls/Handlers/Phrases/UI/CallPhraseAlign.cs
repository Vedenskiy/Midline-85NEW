using System;
using TMPro;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases.UI
{
    public class CallPhraseAlign : MonoBehaviour
    {
        [SerializeField] private bool _isRightMapping = false;
        [SerializeField] private int _mappingSize = 300;
        
        [SerializeField] private TextMeshProUGUI _personName;
        [SerializeField] private TextMeshProUGUI _personMessage;

        private void Start() => 
            Align(_isRightMapping);

        public void Align(bool isRightMapping)
        {
            _isRightMapping = isRightMapping;
            
            _personName.alignment = GetAlignmentOptions(isRightMapping);
            
            _personMessage.margin = GetAlignMargin(isRightMapping);
            _personMessage.alignment = GetAlignmentOptions(isRightMapping);
        }

        private Vector4 GetAlignMargin(bool isRightMapping) =>
            isRightMapping 
                ? new Vector4(_mappingSize, 0, 0, 0) 
                : new Vector4(0, 0, _mappingSize, 0);

        private TextAlignmentOptions GetAlignmentOptions(bool isRightMapping) => 
            isRightMapping 
                ? TextAlignmentOptions.Right 
                : TextAlignmentOptions.Left;
    }
}