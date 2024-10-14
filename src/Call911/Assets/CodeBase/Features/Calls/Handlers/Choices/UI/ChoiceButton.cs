using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Features.Calls.Handlers.Choices.UI
{
    public class ChoiceButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _message;
        [SerializeField] private Button _button;

        private string _choiceId;

        public event Action<string> Pressed; 

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonPressed);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonPressed);
        
        public void Setup(string choiceId)
        {
            _choiceId = choiceId;
            _message.text = choiceId;
        }
        
        private void OnButtonPressed() => 
            Pressed?.Invoke(_choiceId);
    }
}