using UnityEngine;

namespace CodeBase.Features.Office.Interactions
{
    public class InteractionDetector : MonoBehaviour
    {
        [SerializeField] private float _distance = 1f;
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private float _height = 1f;
        [SerializeField] private LayerMask _targetLayer;

        public IInteractable CurrentInteractable { get; private set; }
        private InteractionHighlight _highlight;

        private readonly Collider[] _colliders = new Collider[1];
        
        public void Detect()
        {
            var hits = Physics.OverlapSphereNonAlloc(GetCenterPoint(), _radius, _colliders, _targetLayer);

            if (hits == 0)
            {
                Clear();
                return;
            }
            
            var target = _colliders[0];

            if (!target.TryGetComponent(out IInteractable interactable))
                return;

            if (CurrentInteractable == interactable)
                return;
            
            Clear();
            
            CurrentInteractable = interactable;
            _highlight = target.GetComponent<InteractionHighlight>();
            
            if (_highlight)
                _highlight.EnableHighlight();
        }

        public void Clear()
        {
            if (_highlight)
            {
                _highlight.DisableHighlight();
                _highlight = null;
            }
            
            CurrentInteractable = null;
        }

        private Vector3 GetCenterPoint() => 
            transform.position 
            + Vector3.up * _height 
            + transform.forward * _distance;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            if (CurrentInteractable != null)
                Gizmos.color = Color.green;
            
            Gizmos.DrawWireSphere(GetCenterPoint(), _radius);
        }
    }
}