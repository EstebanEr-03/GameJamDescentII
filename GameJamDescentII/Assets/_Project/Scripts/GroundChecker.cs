using UnityEngine;

namespace DescentII
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] float sphereRadius = 0.1f;
        [SerializeField] float groundDistance = 0.5f;
        [SerializeField] float pivotOffset = 0.1f;
        [SerializeField] LayerMask groundLayer;

        private Vector3 SphereCastOrigin
            => transform.position + Vector3.up * (sphereRadius + pivotOffset);

        public bool IsGrounded { get; private set; }

        private void Update()
        {
            IsGrounded = Physics.SphereCast(
                SphereCastOrigin,        
                sphereRadius,   
                Vector3.down,  
                out RaycastHit _, 
                groundDistance, 
                groundLayer 
            );
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded ? Color.green : Color.red;
            
            Vector3 endPosition = SphereCastOrigin + Vector3.down * (groundDistance + sphereRadius);
            
            Gizmos.DrawWireSphere(SphereCastOrigin, sphereRadius);
            Gizmos.DrawWireSphere(endPosition, sphereRadius);
            Gizmos.DrawLine(SphereCastOrigin, endPosition);
        }
    }
}