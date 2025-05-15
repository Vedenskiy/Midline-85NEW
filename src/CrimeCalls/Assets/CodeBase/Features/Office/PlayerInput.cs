using UnityEngine;

namespace CodeBase.Features.Office
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private TiledMovement _movement;
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
                _movement.Move(transform.forward);
            else if (Input.GetKey(KeyCode.S))
                _movement.Move(-transform.forward);
            
            if (Input.GetKey(KeyCode.D))
                _movement.Rotate(transform.right);
            else if (Input.GetKey(KeyCode.A))
                _movement.Rotate(-transform.right);
        }
    }
}