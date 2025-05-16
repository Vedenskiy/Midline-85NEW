using System;
using UnityEngine;

namespace CodeBase.Features.Office.Interactions
{
    public class SwitchInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animationTriggerName = "Activate";

        private bool _isInteracted = false;
        
        public void Interact(GameObject interactor)
        {
            if (_isInteracted)
                return;

            _isInteracted = true;
            _animator.SetTrigger(_animationTriggerName);
        }
    }
}