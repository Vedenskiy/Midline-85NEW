using System.Collections.Generic;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Choices.UI
{
    public class ChoicesUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private ChoiceButton _buttonPrefab;
        [SerializeField] private CanvasGroup _canvas;

        private Dictionary<string, ChoiceButton> _buttons = new();

        private PlayerChoices _choices;
        
        [Inject]
        public void Construct(PlayerChoices choices) => 
            _choices = choices;

        private void OnEnable()
        {
            _choices.ChoicesShown += OnChoicesShown;
            _choices.ChoicesHide += OnChoicesHide;
        }
        
        private void OnDisable()
        {
            _choices.ChoicesShown -= OnChoicesShown;
            _choices.ChoicesHide -= OnChoicesHide;
        }
        
        private void OnChoicesShown(IReadOnlyCollection<ChoiceData> choices)
        {
            foreach (var choiceData in choices)
            {
                var instance = Instantiate(_buttonPrefab, _container);
                instance.GetComponentInChildren<TextMeshProUGUI>().text = choiceData.ChoiceId;
                instance.Pressed += OnButtonPressed;
                _buttons.Add(choiceData.ChoiceId, instance);
            }

            _canvas.alpha = 1f;
        }
        
        private void OnChoicesHide()
        {
            foreach (var button in _buttons.Values)
            {
                button.Pressed -= OnButtonPressed;
                Destroy(button.gameObject);
            }
            
            _buttons.Clear();
            _canvas.alpha = 0f;
        }

        private void OnButtonPressed(string choiceId) => 
            _choices.Choice(choiceId);
    }
}