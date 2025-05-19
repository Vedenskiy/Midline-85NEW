using FronkonGames.TinyTween.Easing;
using FronkonGames.TinyTween.Extensions;
using UnityEngine;

namespace CodeBase.Features.Office
{
    public class TiledMovement : MonoBehaviour
    {
        [SerializeField] private float _stepLength = 1f;
        [SerializeField] private float _stepDuration = 0.25f;
        [SerializeField] private float _rotateDuration = 0.25f;
        
        private bool _isProcessing;

        public void Move(Vector3 direction)
        {
            if (_isProcessing)
                return;
            
            _isProcessing = true;
            
            transform
                .TweenMove(transform.position + direction * _stepLength, _stepDuration, Ease.Linear)
                .OnEnd(_ => _isProcessing = false);
        }

        public void Rotate(Vector3 direction)
        {
            if (_isProcessing)
                return;
            
            _isProcessing = true;
            
            transform
                .TweenRotation(Quaternion.LookRotation(direction, Vector3.up), _rotateDuration, Ease.Linear)
                .OnEnd(_ => _isProcessing = false);
        }
    }
}