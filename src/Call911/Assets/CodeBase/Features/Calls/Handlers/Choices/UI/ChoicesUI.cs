using System.Collections.Generic;
using CodeBase.Infrastructure.Common.Localization;
using FronkonGames.TinyTween;
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
        private LocalizationService _localization;
        
        [Inject]
        public void Construct(PlayerChoices choices, LocalizationService localization)
        {
            _choices = choices;
            _localization = localization;
        }

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
            CreateButtons(choices);
            _canvas.TweenAlpha(0f, 1f, 1f, Ease.Linear);
        }
        
        private void OnChoicesHide()
        {
            _canvas.TweenAlpha(1f, 0f, 1f, Ease.Linear).OnEnd((_) =>
            {
                ClearButtons();
            });
        }

        private void CreateButtons(IEnumerable<ChoiceData> choices)
        {
            foreach (var choiceData in choices)
            {
                var instance = Instantiate(_buttonPrefab, _container);
                instance.Setup(choiceData.ChoiceId, _localization.GetTranslatedString(choiceData.ChoiceId));
                instance.Pressed += OnButtonPressed;
                _buttons.Add(choiceData.ChoiceId, instance);
            }
        }

        private void ClearButtons()
        {
            foreach (var button in _buttons.Values)
            {
                button.Pressed -= OnButtonPressed;
                Destroy(button.gameObject);
            }

            _buttons.Clear();
        }

        private void OnButtonPressed(string choiceId) => 
            _choices.Choice(choiceId);
    }
}