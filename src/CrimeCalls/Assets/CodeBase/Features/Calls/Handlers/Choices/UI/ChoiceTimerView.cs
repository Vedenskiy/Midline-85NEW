using System;
using FronkonGames.TinyTween;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Features.Calls.Handlers.Choices.UI
{
    public class ChoiceTimerView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private ChoiceProgressBar _progressBar;
        [SerializeField] private int _amountOfIconRotation = 3;

        private ChoiceTimer _timer;

        private float _currentProgress;
        private float _desiredProgress;

        private Tween<float> _progressTween;

        [Inject]
        public void Construct(ChoiceTimer timer) => 
            _timer = timer;

        private void OnEnable()
        {
            _timer.Started += OnTimerStarted;
            _timer.Stopped += OnTimerStopped;
        }

        private void OnDisable()
        {
            _timer.Started -= OnTimerStarted;
            _timer.Stopped -= OnTimerStopped;
        }

        private void OnTimerStopped() => 
            _progressTween.Stop(moveToEnd: false);

        private void OnTimerStarted() =>
            _progressTween = TweenFloat
                .Create()
                .Origin(0)
                .Destination(1)
                .Duration(_timer.Duration)
                .Easing(Ease.Linear)
                .OnUpdate(value => _progressBar.SetProgress(value.Value))
                .Start();
    }
}