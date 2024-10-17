using FronkonGames.TinyTween;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Features.Calls.Handlers.Choices.UI
{
    public class ChoiceTimer : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private ChoiceProgressBar _progressBar;
        [SerializeField] private int _amountOfIconRotation = 3;

        private PlayerChoices _choices;

        private float _currentProgress;
        private float _desiredProgress;

        [Inject]
        public void Construct(PlayerChoices choices) => 
            _choices = choices;

        private void OnEnable() => 
            _choices.TimerStarted += OnProgressUpdated;

        private void OnDisable() => 
            _choices.TimerStarted -= OnProgressUpdated;

        private void OnProgressUpdated(float duration) =>
            duration
                .Tween(0, 1, duration, Ease.Linear)
                .OnUpdate((value) => _progressBar.SetProgress(value.Value));
    }
}