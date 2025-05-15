using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Features.Office
{
    public class TiledPlayerInput : MonoBehaviour
    {
        [SerializeField] private TiledMovement _movement;
        
        private InputSystem _system;

        private void Awake() => 
            _system = new InputSystem();

        private void OnEnable()
        {
            _system.Movement.MoveForward.performed += OnMoveForward;
            _system.Movement.MoveBackward.performed += OnMoveBackward;
            _system.Movement.RotateLeft.performed += OnRotateLeft;
            _system.Movement.RotateRight.performed += OnRotateRight;
            
            _system.Enable();
        }
        
        private void OnDisable()
        {
            _system.Disable();
            
            _system.Movement.MoveForward.performed -= OnMoveForward;
            _system.Movement.MoveBackward.performed -= OnMoveBackward;
            _system.Movement.RotateLeft.performed -= OnRotateLeft;
            _system.Movement.RotateRight.performed -= OnRotateRight;
        }

        private void OnMoveBackward(InputAction.CallbackContext context) => 
            _movement.Move(-transform.forward);

        private void OnMoveForward(InputAction.CallbackContext context) => 
            _movement.Move(transform.forward);

        private void OnRotateRight(InputAction.CallbackContext context) => 
            _movement.Rotate(transform.right);

        private void OnRotateLeft(InputAction.CallbackContext context) => 
            _movement.Rotate(-transform.right);
    }
}